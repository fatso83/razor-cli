RAZOR_PATH=Microsoft.AspNet.Razor.3.0.0/lib/net45
RAZORENG_PATH=RazorEngine.3.8.2/lib/net45
JSON_PATH=Newtonsoft.Json.8.0.3/lib/net45
MONO_PATH=${RAZORENG_PATH}:${RAZOR_PATH}:${JSON_PATH}

# default target
all: razor-cli.exe

# Targets with no real dependencies on files
.PHONY: all dependencies clean  nuget

nuget: 
	curl https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -o nuget.exe

# Not sure how to add this target as a dependency while avoiding 
# running nuget every time
dependencies: nuget
	mono nuget.exe install RazorEngine && mono nuget.exe install Newtonsoft.Json

razor-cli.exe: razor-cli.cs
	mcs /reference:${RAZORENG_PATH}/RazorEngine.dll /reference:${JSON_PATH}/Newtonsoft.Json.dll razor-cli.cs  

run: all
	MONO_PATH="${MONO_PATH}" mono razor-cli.exe partial.example.cshtml model.example.json

clean: 
	rm -f razor-cli.exe

clean-libs:
	rm -rf Microsoft.AspNet.Razor.3.0.0/ RazorEngine.3.8.2/ 

clean-nuget:
	rm -f nuget.exe

clean-all: clean clean-libs clean-nuget

rebuild: clean all
