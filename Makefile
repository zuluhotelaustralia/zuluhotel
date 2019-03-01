RUNTIME=dotnet build
OUTPUTDIR=-o ./

.DELETE_ON_ERROR:

release:
	date +%y.%m.%d.%H | awk -f Tools/version.awk
	$(RUNTIME) $(OUTPUTDIR)

run: release
	dotnet ./RunZH.dll

debug:
	date +%y.%m.%d.%H | awk -f Tools/version.awk
	$(RUNTIME) $(OUTPUTDIR)

clean:
	-rm RunZH.deps.json
	-rm RunZH.dll
	-rm RunZH.pdb
	-rm RunZH.runtimeconfig.dev.json
	-rm RunZH.runtimeconfig.json
	-rm Server/Version.cs
