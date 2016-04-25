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

### Known issues
- no `@Html` helpers in the scope. Only simple `@model` accesses.
