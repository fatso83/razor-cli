// See also tips on building cli apps with razorengine: https://github.com/Antaris/RazorEngine/blob/master/src/source/RazorEngine.Hosts.Console/RazorEngine.Hosts.Console.csproj
using System;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Moq;
using System.IO;
using Newtonsoft.Json.Linq;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.
using RazorEngine.Configuration;
using RazorEngine.Text;

public class RazorCli
{
    static public void Main (string[] args)
    {
        CheckCommandLine(args);

        string template = ReadFile(args[0]);
        JObject model = ParseModel(args[1]);

        // try to load the required assemblies
        //http://stackoverflow.com/a/23496144/200987
        System.Reflection.Assembly.Load("System.Web");
        System.Reflection.Assembly.Load("System.Web.Mvc");

        var result = CompileTemplate(template, model);

        Console.WriteLine (result);
    }

    private static string CompileTemplate (string template, JObject model)
    {
        string res = "";
        var config = new TemplateServiceConfiguration();
        // You can use the @inherits directive instead (this is the fallback if no @inherits is found).
        config.BaseTemplateType = typeof(MyClassImplementingTemplateBase<>);
        try 
        {
            using (var service = RazorEngineService.Create(config))
            {
                res = service.RunCompile(template, "templateKey", null, model);
            }
        } 
        catch( RazorEngine.Templating.TemplateCompilationException ex ) 
        {
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


public class MyHtmlHelper
{
    public IEncodedString Raw(string rawString)
    {
        return new RawString(rawString);
    }
}

// https://antaris.github.io/RazorEngine/TemplateBasics.html
public abstract class MyClassImplementingTemplateBase<T> : TemplateBase<T>
{
    public MyClassImplementingTemplateBase()
    {
        Html = MvcHelpers.CreateHtmlHelper<Object>();
    }

    public HtmlHelper Html { get; set; }

}


// Ripped straight from a SO Q/A
// http://stackoverflow.com/questions/17271688/mocking-viewcontext-to-test-validation-error-messages
public class MvcHelpers {
    public static HtmlHelper<TModel> CreateHtmlHelper<TModel>(ViewDataDictionary dictionary = null)
    {
        if (dictionary == null)
            dictionary = new ViewDataDictionary { TemplateInfo = new TemplateInfo() };

        var mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object),
                new Mock<IView>().Object,
                dictionary,
                new TempDataDictionary(),
                new Mock<TextWriter>().Object);

        var mockViewDataContainer = new Mock<IViewDataContainer>();
        mockViewDataContainer.Setup(v => v.ViewData).Returns(dictionary);

        return new HtmlHelper<TModel>(mockViewContext.Object, mockViewDataContainer.Object);
    }
}
