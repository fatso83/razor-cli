RAZOR_PATH=lib/Microsoft.AspNet.Razor.3.0.0/lib/net45
RAZORENG_PATH=lib/RazorEngine.3.8.2/lib/net45
JSON_PATH=lib/Newtonsoft.Json.8.0.3/lib/net45
MONO_PATH=${RAZORENG_PATH}:${RAZOR_PATH}:${JSON_PATH}
NUGET_CMD=mono tools/nuget.exe

# default target
all: razor-cli.exe

# Targets with no real dependencies on files
.PHONY: all dependencies clean  nuget

update-nuget: 
	[ -e tools/nuget.exe ] || curl https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -o tools/nuget.exe

dependencies: packages.config update-nuget
	${NUGET_CMD} restore -PackagesDirectory lib

razor-cli.exe: razor-cli.cs
	mcs /reference:${RAZORENG_PATH}/RazorEngine.dll /reference:${JSON_PATH}/Newtonsoft.Json.dll razor-cli.cs  

run: all
	MONO_PATH="${MONO_PATH}" mono razor-cli.exe partial.example.cshtml model.example.json

clean: 
	rm -f razor-cli.exe

clean-libs:
	rm -rf lib

clean-nuget:
	rm -f tools/nuget.exe

clean-all: clean clean-libs clean-nuget

rebuild: clean all
