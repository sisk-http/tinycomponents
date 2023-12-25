# TinyComponents

Welcome to TinyComponents! This tiny library will help you create HTML components with a readable language that interacts with your code.

This library is aimed at rendering HTML components securely, assigning attributes, classes, IDs and more to your elements securely, building your HTML in an elegant and formal way.

```csharp
HtmlElement MyComponent(string name)
{
    return new HtmlElement("div")
        .WithContent($"Hey! Im {name}.");
}

string? html = MyComponent("Dave").Render();

Console.WriteLine(html);
```

Should output:

```html
<div>Hey! Im Dave.</div>
```

The library is extremely simple to use, covering just a few very simple functions. This file will document all your resources here.

To correctly use this library, it is also interesting to note what it is and what it is not. TinyComponents **is** an library to help rendering HTML with an fluent and elegant syntax, which also support basic templating and components, but **it's not** an complete frontend framework with styles and scripts.

## The IRenderable

Every renderable element inherits from `IRenderable`, which calls the render method and creates a string. This interface is not limited to HTML elements.

```csharp
record MyRenderable(string Name) : IRenderable
{
    public string? Render()
    {
        return $"Hello, {Name}";
    }
}

string? hello = new MyRenderable("world").Render();
Console.WriteLine(hello);
// Hello, world
```

The `HtmlElement` class already implements this interface and has methods that will help you build your objects. You can create reusable elements, better known as "components". There are several functions to do this. Initially we will address the primitive capabilities of an HtmlElement.

```csharp
var myDiv = new HtmlElement("div")
    .WithClass("form-control", "bg-primary")
    .WithId("my-button")
    .WithAttribute("onclick", "foo()")
    .WithName("submit-button")
    .WithStyle(new { color = "red", backgroundColor = "white" })
    .WithContent("Click me!");

Console.WriteLine(myDiv.Render());
```

Shoud output:

```html
<div onclick="foo()"
    id="my-button"
    name="submit-button"
    class="form-control bg-primary"
    style="color:red;background-color:white;">

    Click me!
</div>
```

> **Note:**
>
> The rendered HTML is always minified, but to improve our examples we will make them more readable and formatted.

Explanation of some of the used fluent methods:

- `WithClass` specifies one or more class to add to the HTML element.
- `WithId` specifies the HTML element id attribute.
- `WithAttribute` specifies one attribute and their value. If the attribute value is an boolean, it will add as `name="name"` when the value is `true`, but if the value is `false` or `null`, it will not add the attribute value.
- `WithAttributes` specifies an anonymous object which will cast every property name casing from `camelCase` to `kebab-case`.
- `WithName` specifies the HTML element name attribute.
- `WithStyle` implies for the `style` tag styling, with same naming rules as `WithAttributes`.
- `WithContent` expects one of these arguments: an string, an `IRenderable` object or an action of self tag, example:

```csharp
var myDiv = new HtmlElement("div")
    .WithContent(div =>
    {
        div += new HtmlElement("span")
            .WithContent("hello");
        div += new HtmlElement("dd")
            .WithContent("world");
    });
```

Which renders to:

```html
<div>
    <span>
        hello
    </span>
    <dd>
        world
    </dd>
</div>
```

## Creating renderables elements

Every way of rendering an `IRenderable` is valid, however, these are the most common:

### Creating an functional component

```csharp
static IRenderable HelloDiv(string name)
{
    return new HtmlElement("div")
        .WithClass("my-class")
        .WithContent($"Hello, {name}!");
}

var baseHtml = new HtmlElement("html")
    .WithContent(html =>
    {
        html += HelloDiv("world");
    });

Console.WriteLine(baseHtml.Render());
// <html><div class="my-class">Hello, world!</div></html>
```

### Creating an class which implements HtmlElement

```csharp
class MyComponent : HtmlElement
{
    public MyComponent() : base("main")
    {
        WithContent(main =>
        {
            main += new HtmlElement("button")
                .WithContent("click me");
        });
    }
}

var component = new MyComponent();
Console.WriteLine(component.Render());
// <main><button>click me</button></main>
```

### Rendering an entire HTML page

```csharp
class HtmlPage : HtmlElement
{
    public string Lang { get; set; } = "en";
    public string DocumentTitle { get; set; }
    public HtmlElement Body { get; set; } = new HtmlElement();

    public HtmlPage(string title) : base("html")
    {
        DocumentTitle = title;
    }

    public HtmlPage WithBody(Action<HtmlElement> handler)
    {
        handler(Body);
        return this;
    }

    public override string? Render()
    {
        WithAttribute("lang", Lang);
        WithContent(html =>
        {
            html += new HtmlElement("head", head =>
            {
                head += new HtmlElement("meta")
                    .WithAttribute("charset", "UTF-8")
                    .SelfClosed();
                head += new HtmlElement("meta")
                    .WithAttributes(new { name = "viewport", content = "width=device-width, initial-scale=1.0" })
                    .SelfClosed();
                head += new HtmlElement("title", DocumentTitle);
            });
            html += new HtmlElement("body", Body);
        });
        return base.Render();
    }
}

HtmlPage page = new HtmlPage("Document")
    .WithBody(body =>
    {
        body += new HtmlElement("p")
            .WithContent("Hello, world!");
    });

Console.WriteLine(page.Render());
```

```html
<html lang="en">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Document</title>
    </head>

    <body>
        <p>Hello, world!</p>
    </body>
</html>
```

### Reusing an page template using abstract

```csharp
abstract class MyHtmlTemplate : HtmlElement
{
    public abstract string DocumentTitle { get; set; }
    public abstract IRenderable RenderBody();

    public MyHtmlTemplate() : base("html")
    {
    }

    public override string? Render()
    {
        // the following syntax use predefined static methods
        // which aims to build predefined html elements.
        WithContent(html =>
        {
            html += Head(head =>
            {
                head += Title(DocumentTitle);
            });
            html += Body(body =>
            {
                body += RenderBody();
            });
        });
        return base.Render();
    }
}

class PageUsingTemplate : MyHtmlTemplate
{
    public override string DocumentTitle { get; set; } = "My super page";

    public override IRenderable RenderBody()
    {
        return Fragment(_ =>
        {
            _ += HtmlElement.Paragraph("Hello from the extending template!");
        });
    }
}

var myPage = new PageUsingTemplate();
Console.WriteLine(myPage.Render());
```

```html
<html>
    <head>
        <title>My super page</title>
    </head>

    <body>
        <p>Hello from the extending template!</p>
    </body>
</html>
```
