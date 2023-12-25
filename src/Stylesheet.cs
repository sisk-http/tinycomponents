using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TinyComponents
{
    internal class Stylesheet : IRenderable
    {
        public ICollection<CssRule> Rules { get; set; } = new List<CssRule>();

        public static Stylesheet FromObject(object style)
        {
            Stylesheet styles = new Stylesheet();
            CssRule n = new CssRule();
            foreach (PropertyInfo pi in style.GetType().GetProperties())
            {
                string name = pi.Name;
                object? value = pi.GetValue(style, null);
                if (value == null) continue;
                string valueConverted = Convert(value);
                n.Properties.Add(name, valueConverted);
            }

            styles.Rules.Add(n);

            return styles;
        }

        public String? Render()
        {
            StringBuilder sb = new StringBuilder();

            RenderRules("", Rules, sb);

            return sb.ToString();
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
                if (rule.Properties.Count > 0)
                {
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

            // check if string already is in kebab case
            if (source.All(c => c == '-' || (char.IsLetter(c) && char.IsLower(c)) || char.IsDigit(c)))
            {
                return source;
            }

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

        public class CssRule
        {
            public string Selector { get; set; } = "";
            public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
            public ICollection<CssRule> Childrens { get; set; } = new List<CssRule>();
        }
    }
}