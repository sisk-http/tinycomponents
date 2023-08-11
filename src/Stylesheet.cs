using System.Collections.Specialized;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace TypedComponents;

public class Stylesheet : IRenderable
{
    private Dictionary<string, Stylesheet> atGroupRules = new Dictionary<string, Stylesheet>();
    private HashSet<Type> definedTypes = new HashSet<Type>();
    public ICollection<CssRule> Rules { get; set; } = new List<CssRule>();

    public static Stylesheet Create(Action<Stylesheet> action)
    {
        Stylesheet styles = new Stylesheet();
        action(styles);
        return styles;
    }

    public static Element StyleTag(Action<Stylesheet> action)
    {
        Stylesheet styles = new Stylesheet();
        action(styles);
        string? css = styles.Render();
        Element scriptTag = new Element("style", style => style += new Text(css, false));
        return scriptTag;
    }

    public CssRule Rule(string rootSelector, Action<dynamic> action)
    {
        CssRule rule = new CssRule()
        {
            Selector = rootSelector,
        };
        action(rule);
        Rules.Add(rule);
        return rule;
    }

    public static Stylesheet FromObject(object style)
    {
        Stylesheet styles = new Stylesheet();
        CssRule n = new CssRule();
        foreach (PropertyInfo pi in style.GetType().GetProperties())
        {
            string name = pi.Name;
            object? value = pi.GetValue(style, null);
            if (value == null) continue;
            name = PascalToKebabCase(name);
            string valueConverted = Convert(value);
            n.Properties.Add(name, valueConverted);
        }

        styles.Rules.Add(n);

        return styles;
    }

    public Element AsStyleElement()
    {
        string? css = this.Render();
        Element scriptTag = new Element("style", style => style += new Text(css, false));
        return scriptTag;
    }

    public void Include(Stylesheet styles)
    {
        foreach (var styleRule in styles.Rules)
        {
            this.Rules.Add(styleRule);
        }
        foreach (var groupRule in styles.atGroupRules)
        {
            this.atGroupRules.Add(groupRule.Key, groupRule.Value);
        }
    }

    public void Include(Action<Stylesheet> styles)
    {
        Include(Create(styles));
    }

    public void Define(object instance, Action<Stylesheet> styles)
    {
        Type t = instance.GetType();
        if (definedTypes.Contains(t))
        {
            return;
        }
        definedTypes.Add(t);
        Include(Create(styles));
    }

    public void IncludeAtGroup(string atRuleDeclaration, Action<Stylesheet> builder)
    {
        Stylesheet s = new Stylesheet();
        builder(s);
        this.atGroupRules.Add(atRuleDeclaration, s);
    }

    public String? Render()
    {
        StringBuilder sb = new StringBuilder();

        RenderRules("", Rules, sb);
        RenderAtGroups(sb);

        return sb.ToString();
    }

    private void RenderAtGroups(StringBuilder sb)
    {
        foreach (KeyValuePair<string, Stylesheet> atGroup in atGroupRules)
        {
            sb.Append('@' + atGroup.Key + '{');
            string? css = atGroup.Value.Render();
            sb.Append(css);
            sb.Append("}");
        }
    }

    private void RenderRules(string selector, IEnumerable<CssRule> rules, StringBuilder sb)
    {
        foreach (var rule in rules)
        {
            string joinSelector;
            if (rule.Selector?.StartsWith('&') == true)
            {
                joinSelector = $"{selector}{rule.Selector.Substring(1)}";
            }
            else
            {
                joinSelector = $"{selector} {rule.Selector}";
            }
            if (rule.Selector != null)
            {
                sb.Append(joinSelector.Trim());
                sb.Append('{');
            }
            foreach (KeyValuePair<string, object> property in rule.Properties)
            {
                sb.Append(PascalToKebabCase(property.Key));
                sb.Append(':');
                string valueConvert = Convert(property.Value);
                sb.Append(valueConvert);
                sb.Append(';');
            }
            if (rule.Selector != null)
            {
                sb.Append('}');
            }
            RenderRules(joinSelector, rule.Childrens, sb);
        }
    }

    internal static string Convert(object value)
    {
        if (value is double d)
        {
            return d.ToString("#.##", CultureInfo.InvariantCulture);
        }
        else
        {
            return value.ToString() ?? "";
        }
    }

    // https://stackoverflow.com/a/54012346/4698166
    internal static string PascalToKebabCase(string source)
    {
        if (source.Length == 0) return string.Empty;

        StringBuilder builder = new StringBuilder();

        for (var i = 0; i < source.Length; i++)
        {
            if (char.IsLower(source[i])) // if current char is already lowercase
            {
                builder.Append(source[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToLower(source[i]));
            }
            else if (char.IsDigit(source[i]) && !char.IsDigit(source[i - 1])) // if current char is a number and the previous is not
            {
                builder.Append('-');
                builder.Append(source[i]);
            }
            else if (char.IsDigit(source[i])) // if current char is a number and previous is
            {
                builder.Append(source[i]);
            }
            else if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(source[i]));
            }
            else if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(source[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(source[i]));
            }
        }
        return builder.ToString();
    }
}