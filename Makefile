RUNTIME=dotnet build
OUTPUTDIR=-o ./

.DELETE_ON_ERROR:


run: release
	dotnet run Server.dll

release:
	$(RUNTIME) $(OUTPUTDIR)

debug:
	$(RUNTIME) $(OUTPUTDIR)

mono:
	mcs -unsafe -t:exe -out:Server.exe -nowarn:219,414 -d:MONO -recurse:Server/*.cs

clean:
	rm Server.dll
	rm Server.pdb
	rm Server.deps.json
	rm Server.runtimeconfig.json
	rm Server.runtimeconfig.dev.json
