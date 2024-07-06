# TinyComponents

Welcome to TinyComponents! This tiny library will help you create XML and HTML components with a readable language that interacts with your code.

This library is aimed at rendering HTML/XML components securely, assigning attributes, classes, IDs and more to your elements, building your HTML in an elegant and formal syntax.

```csharp
HtmlElement MyComponent(string name)
{
    return new HtmlElement("div")
        .WithContent($"Hey! Im {name}.");
}

var element = MyComponent("Dave");
Console.WriteLine(element);
```

Should output:

```html
<div>Hey! Im Dave.</div>
```

The library is extremely simple to use, covering just a few very simple functions. This file will document all your resources here.

To correctly use this library, it is also interesting to note what it is and what it is not. TinyComponents **is** an library to help rendering XML/HTML with an fluent and functional syntax, which also support basic templating and components, but **it's not** an complete frontend framework with styles and scripts.

## Installing

Install the **experimental version** of TinyComponents with at [Nuget](https://www.nuget.org/packages/TinyComponents):

```
dotnet add package TinyComponents
```

## Creating components

All components are rendered through `ToString()` present in each object. Any object can be rendered inside an HtmlElement or XmlNode. There are three primitive types for creating objects:

- `TinyComponents.HtmlElement`
- `TinyComponents.XmlNode`
- `TinyComponents.RenderableText`

HtmlElement and XmlNode have very similar renderers, with the difference that:

- `HtmlElement` has the `Id`, `Name`, `TabIndex`, `Style`, `Title` and `ClassList` properties.
- When rendering `HtmlElement` with `SelfClosed = true`, XML is terminated with `/>` while HTML `>`.

### Example 1:

```csharp
var ul = new HtmlElement("ul");
ul.Style = new { backgroundColor = "red" };
ul.Id = "fruit-list";

ul.Children.Add(new HtmlElement("li", "Apple"));
ul.Children.Add(new HtmlElement("li", "Mango"));
ul.Children.Add(new HtmlElement("li", "Blueberry"));

Console.WriteLine(ul.ToString());
```

Renders to:

```html
<ul id="fruit-list" style="background-color:red;">
    <li>Apple</li>
    <li>Mango</li>
    <li>Blueberry</li>
</ul>
```

### Example 2:

```csharp
public class Li : HtmlElement
{
    public Li(string content) : base("li", content)
    {
    }
}

var ul = new HtmlElement("ul");
ul.Style = new { backgroundColor = "red" };
ul.Id = "fruit-list";

ul.Children.Add(new Li("Apple"));
ul.Children.Add(new Li("Mango"));
ul.Children.Add(new Li("Blueberry"));

Console.WriteLine(ul.ToString());
```

```html
<ul id="fruit-list" style="background-color:red;">
    <li>Apple</li>
    <li>Mango</li>
    <li>Blueberry</li>
</ul>
```

### Example 3:

```csharp
var form = new HtmlElement("form")
    .WithAttribute("method", "POST")
    .WithAttribute("action", "/contact.php")
    .WithContent(form =>
    {
        form += new HtmlElement("input")
            .SelfClosed()
            .WithName("name")
            .WithAttribute("type", "text")
            .WithAttribute("required")
            .WithAttribute("placeholder", "Your name");

        form += new HtmlElement("input")
            .SelfClosed()
            .WithName("message")
            .WithAttribute("type", "text")
            .WithAttribute("required")
            .WithAttribute("placeholder", "Message");

        form += new HtmlElement("button")
            .WithContent("Send message")
            .WithAttribute("type", "submit");
    });

Console.WriteLine(form);
```

```html
<form method="POST" action="/contact.php">
    <input type="text" required placeholder="Your name" name="name">
    <input type="text" required placeholder="Message" name="message">
    <button type="submit">
        Send message
    </button>
</form>
```

### Example 4:

```csharp
var name = "<John>";
var formattableText = HtmlElement.Format($"""
    <h1>Hello, {name}!</h1>
    """);

Console.WriteLine(formattableText);
```

```html
<h1>Hello, &lt;John&gt;!</h1>
```

### Example 5:

```csharp
class BlueButton : HtmlElement
{
    public BlueButton(string text)
    {
        TagName = "button";
        Style = new
        {
            backgroundColor = "blue",
            color = "white",
            border = "1px solid darkblue"
        };
        Children = [new RenderableText(text)];
    }
}

var button = new BlueButton("Click me!");
Console.WriteLine(button);
```

```html
<button style="background-color:blue;color:white;border:1px solid darkblue;">Click me!</button>
```

### Example 6:

```csharp
static HtmlElement Html() => new HtmlElement("html");
static HtmlElement Body() => new HtmlElement("body");
static HtmlElement Ul() => new HtmlElement("ul");
static HtmlElement Li(string text) => new HtmlElement("li", text);
static HtmlElement H1(string text) => new HtmlElement("h1", text);

static void Main(string[] args)
{
    string[] colors = ["Red", "Blue", "Yellow"];

    var page = Html().WithContent(html =>
        {
            html += Body().WithContent(body =>
            {
                body += H1("List of colors:");

                var colorList = Ul();
                foreach (string color in colors)
                    colorList += Li(color);

                body += colorList;
            });
        });

    Console.WriteLine(page);
}
```

```html
<html>
    <body>
        <h1>List of colors:</h1>
        <ul>
            <li>Red</li>
            <li>Blue</li>
            <li>Yellow</li>
        </ul>
    </body>
</html>
```