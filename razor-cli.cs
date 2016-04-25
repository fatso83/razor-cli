using System;
using System.IO;
using Newtonsoft.Json.Linq;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.

public class RazorCli
{
    static public void Main (string[] args)
    {
        CheckCommandLine(args);

        string template = ReadFile(args[0]);
        JObject model = ParseModel(args[1]);

        var result = CompileTemplate(template, model);

        Console.WriteLine (result);
    }

    private static string CompileTemplate (string template, JObject model)
    {
        string res = "";
        try {
            res = Engine.Razor.RunCompile(template, "templateKey", null, model);
        } catch( RazorEngine.Templating.TemplateCompilationException ex ) {
            Console.WriteLine (ex);
            System.Environment.Exit(1);
        }
        return res;
    }

    /* Cannot dispatch a dynamic object to extension methods */
    private static JObject ParseModel(string fileName){
        string json = ReadFile(fileName);
        return JObject.Parse(json);
    }

    private static void CheckCommandLine(string[] args){
        if(args.Length != 2){
            Usage();
            System.Environment.Exit(1);
        }
    }

    private static void Usage(){
        string usage = "Usage: razor-cli <partial.cshtml> <model.json>\n";
        Console.WriteLine(usage);
    }

    private static String ReadFile(string filename)
    {
        string result;

        using (StreamReader sr = new StreamReader(filename))
        {
            result = sr.ReadToEnd();
        }

        return result;
    }

}
