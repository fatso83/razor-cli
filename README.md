# razor-cli
> A CLI app to compile Razor templates

This tool is mean to scratch an itch when it comes
to reusing partials from ASP.NET applications in 
other settings (such as styleguides) and keeping 
them in sync.

## Annoyed with the Makefile?
As the tool is made by someone coming from a 
unixy background using a Mac, please excuse
the use of non-Microsoft standard tools such as 
Make. This was just done to get something
out the door, so feel free to supply me with
a pull request that makes a `*.msbuild` file
that can be built with `xbuild` and `MSBuild`.

### Build
We assume you have the Mono runtime in your `$PATH`

```
make dependencies #downloads NuGet and libs
make 
```

You know have a `razor-cli.exe` binary

### Run the exe

You need to have the libs in the `$MONO_PATH`.
See how this works by running `make run`
