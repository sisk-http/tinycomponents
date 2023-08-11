using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TypedComponents;

public class Text : IRenderable
{
    public bool Escape { get; set; } = true;
    public object? Contents { get; set; }

    public Text(object? textContents)
    {
        Contents = textContents;
    }

    public Text(object? textContents, bool escape)
    {
        Contents = textContents;
        Escape = escape;
    }

    public String? Render()
    {
        if (Escape)
        {
            return HttpUtility.HtmlEncode(Contents);
        }
        else
        {
            return Contents?.ToString();
        }
    }
}
