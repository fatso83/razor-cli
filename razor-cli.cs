using System;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.

public class HelloWorld
{
    static public void Main ()
    {
        string template = "Hello @Model.Name, welcome to RazorEngine!";
        var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

        Console.WriteLine (result);
    }
}
