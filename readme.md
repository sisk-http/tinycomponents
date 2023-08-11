# Typed Components

> **Warning**: 
> This is an experimental project. There is not enough documentation yet, or the API, code and project objective may change in the future.

Typed Components is a lightweight library designed to build structures, components and web pages natively from C# code, elegantly, fluently and without relying on a template engine. It's capable of writing HTML pages so that each element is an object within the code itself, and the construction is extremely fast and simple to create. It does not need configuration and the construction of templates and components is done during the application.

From this code:

```cs
static void Main(string[] args)
{
    var createHello = (string message) => Element.Html(_ =>
    {
        _ += Element.Head(_ =>
        {
            _ += Element.Title("Hello from Typed Components!");
        });
        _ += Element.Body(_ =>
        {
            _ += Element.H1(message);
        });
    });

    string? result = createHello("Hello, world!").Render();

    Console.WriteLine(result);
}
```

You can get:

```html
<html>
    <head>
        <title>Hello from Typed Components!</title>
    </head>
    <body>
        <h1>
            Hello, world!
        </h1>
    </body>
</html>
```

In a blink of an eye.

The basic principle of this lib is to interpolate the strings and build them with `IRenderable` elements, which are objects capable of rendering other objects. This library also supports writing CSS in the same elegant and fluent way:

```cs
static void Main(string[] args)
{
    var css = Stylesheet.Create(_ =>
    {
        _.Rule("body", _ =>
        {
            _.Margin = "8px";
            _.BackgroundColor = "white";
            _.Color = "black";

            _["> h1"] = CssRule.Sub(_ =>
            {
                _.FontSize = "18px";

                _["&:hover"] = CssRule.Sub(_ =>
                {
                    _.Color = "red";
                });
            });
        });
    });

    Console.WriteLine(css.Render());
}
```

Which renders:

```css
body {
    margin: 8px;
    background-color: white;
    color: black;
}

body > h1 {
    font-size: 18px;
}

body > h1:hover {
    color: red;
}
```

## But why not use a template engine?

Using a template engine can come at the cost of where to store the template files, configuration and engine rendering cost. This library dispenses with all the items mentioned above. Templates, components or configuration is done by the user, without requiring a design standard or way of writing. Furthermore, it is possible to create elements or styles from the code itself, without dedicating configuration or additional files for this.

## The future of this project

The project is at the stage of understanding whether there is potential to be used and whether it is something that will be easy to maintain in the future or not. Some more primitive and commercial tests will be done to test the sustainability of this project, in addition to the time to maintain it.