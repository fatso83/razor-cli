# razor-cli
> A cross-platform CLI app to compile Razor templates

This tool is mean to scratch an itch when it comes
to reusing partials from ASP.NET applications in 
other settings (such as styleguides) and keeping 
them in sync.

The basic interface is just to be able to use this
as part of a build pipeline. A basic wrapper to
be able to use this with [Node is in place](https://github.com/fatso83/razor-cli-node).

## Usage

```
export MONO_PATH="lib/RazorEngine.3.8.2/lib/net45:lib/Microsoft.AspNet.Razor.3.0.0/lib/net45:lib/Newtonsoft.Json.8.0.3/lib/net45" 

mono razor-cli.exe partial.example.cshtml model.example.json
Hello John Smith, this is an example of what <strong>Razor CLI</strong> can do!
```

You can skip the `mono` bit on Windows, but I could really need 
[a hand](https://github.com/fatso83/razor-cli/issues/2) testing
and documenting this.

### Using libraries in your code such as `Html.Raw`?
The RazorEngine driving this thing is only concerned
with the actual parsing. Any libraries you might
use in your templates [is none of its concern](https://antaris.github.io/RazorEngine/ReferenceResolver.html)
needs to be added to the runtime and made
available for the runtime to use when actually
running the template.

To [solve this I need help](https://github.com/fatso83/razor-cli/issues/1)
from someone with more .NET foo than myself.

## Annoyed with the Makefile? Something else?
This was whipped together in a couple of hours by
someone unfamiliar with the .NET pipeline using
a text editor and the command line on a Mac so it might 
deviate a bit from _best practices_.

See the [issues](https://github.com/fatso83/razor-cli/issues)
for how you can help out.

### Build
We assume you have the Mono runtime and tools in your `$PATH`

```
make dependencies #downloads NuGet and libs
make 
```

You now have a `razor-cli.exe` binary.

## Known issues
- Very little error handling
- Very little help using the cli
- Mono runtime needed issues, even though you have a working .NET runtime on Windows. See [issue #2]((https://github.com/fatso83/razor-cli/issues/2)).
- [No way to use libraries in the templates (#1)](https://github.com/fatso83/razor-cli/issues/1)
