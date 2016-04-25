# razor-cli
> A CLI app to compile Razor templates

This tool is mean to scratch an itch when it comes
to reusing partials from ASP.NET applications in 
other settings (such as styleguides) and keeping 
them in sync.

The basic interface is just to be able to use this
as part of a build pipeline, possibly being wrapped
by other build tools (NPM, Gulp, Grunt, ...).

## Usage

```
export MONO_PATH="lib/RazorEngine.3.8.2/lib/net45:lib/Microsoft.AspNet.Razor.3.0.0/lib/net45:lib/Newtonsoft.Json.8.0.3/lib/net45" 

mono razor-cli.exe partial.example.cshtml model.example.json
Hello John Smith, this is an example of what <strong>Razor CLI</strong> can do!
```

It might be possible to skip the `mono` bit on Windows(?).

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

Please excuse the use of non-Microsoft standard 
tools such as Make. This was just done to get something
out the door, so feel free to supply me with
a pull request that makes a `*.msbuild` file
that can be built with `xbuild` and `MSBuild`.

If you supply a pull request, please bear in mind
that it needs to be buildable and runnable
on all platforms without using Visual Studio.

### Build
We assume you have the Mono runtime in your `$PATH`

```
make dependencies #downloads NuGet and libs
make 
```

You know have a `razor-cli.exe` binary

## Known issues
- Very little error handling
- Very little help using the cli
- [No way to use libraries in the templates (#1)](https://github.com/fatso83/razor-cli/issues/1)
