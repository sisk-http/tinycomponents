using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypedComponents;

public class CssRule : DynamicObject
{
    public string? Selector { get; set; }
    public Dictionary<string, object> Properties { get; set; } = new ();
    public ICollection<CssRule> Childrens { get; set; } = new List<CssRule>();

    public CssRule this[string selector]
    {
        set
        {
            value.Selector = selector;
            this.Childrens.Add(value);
        }
    }

    public override Boolean TrySetMember(SetMemberBinder binder, Object? value)
    {
        if (value is CssRule r)
        {
            r.Selector = r.Selector;
            this.Childrens.Add(r);
        }
        else
        {
            if(value == null)
            {
                Properties.Remove(binder.Name);
                return true;
            }
            Properties[binder.Name] = value;
        }
        return true;
    }

    public override Boolean TryGetMember(GetMemberBinder binder, out Object result)
    {
        result = Properties[binder.Name];
        return true;
    }

    public static CssRule Sub(Action<dynamic> action)
    {
        CssRule r = new CssRule();
        action(r);
        return r;
    }
}