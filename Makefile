BIN_NAME=razor-cli
BUILD=build
BIN=${BUILD}/razor-cli.exe

# Dependencies
RAZOR_PATH=lib/Microsoft.AspNet.Razor.3.0.0/lib/net45/System.Web.Razor.dll
RAZORENG_PATH=lib/RazorEngine.3.8.2/lib/net45/RazorEngine.dll
JSON_PATH=lib/Newtonsoft.Json.8.0.3/lib/net45/Newtonsoft.Json.dll

DEPS=${RAZOR_PATH} ${RAZORENG_PATH} ${JSON_PATH}

NUGET_CMD=tools/nuget
BIN_CMD=${BUILD}/${BIN_NAME}
WRAPPER=${BIN_CMD}

# default target
all: ${BIN} ${WRAPPER}

# Targets with no real dependencies on files
.PHONY: all dependencies clean  nuget

${WRAPPER}: wrapper.sh
	cp wrapper.sh ${BIN_CMD} && chmod 755 ${BIN_CMD} 

update-nuget: 
	[ -e tools/nuget.exe ] || curl -s https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -o tools/nuget.exe

dependencies: packages.config update-nuget
	${NUGET_CMD} restore -PackagesDirectory lib > /dev/null

copy-dependencies: dependencies
	mkdir ${BUILD} 2>/dev/null  || :
	cp ${DEPS} ${BUILD}/ > /dev/null

${BIN}: razor-cli.cs copy-dependencies
	mcs /reference:${RAZORENG_PATH} /reference:${JSON_PATH} razor-cli.cs  -out:${BIN} > /dev/null

run: all
	${BIN_CMD} partial.example.cshtml model.example.json

clean: clean-build

clean-build: 
	rm -rf ${BUILD}

clean-libs:
	rm -rf lib

clean-nuget:
	rm -f tools/nuget.exe

clean-all: clean clean-libs clean-nuget

rebuild: clean all
