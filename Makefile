RUNTIME=dotnet build
FRAMEWORK=-f netcoreapp2.1
CONFIGURATION=-c Release
OUTPUTDIR=-o ../

.DELETE_ON_ERROR:


run: release
	mono Server.exe

release:
	$(RUNTIME) $(FRAMEWORK) $(CONFIGURATION) $(OUTPUTDIR)

debug:
	$(RUNTIME) $(FRAMEWORK) $(OUTPUTDIR)

mono:
	mcs -unsafe -t:exe -out:Server.exe -nowarn:219,414 -d:MONO -recurse:Server/*.cs

clean:
	rm Server.dll
	rm Server.pdb
	rm Server.deps.json
	rm Server.runtimeconfig.json
	rm Server.runtimeconfig.dev.json
