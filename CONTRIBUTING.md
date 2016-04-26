
If you supply a pull request, please bear in mind
that it needs to be buildable and runnable
on all platforms without using Visual Studio.

This is not as bad as it seems. The Mono runtime
is owned by Microsoft and is sharing code with the
.NET project so most stuff just works. 

Just keep away from using Windows specific tooling,
so just test that it works using `xbuild` after
installing the Mono runtime.
