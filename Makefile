RUNTIME=dotnet build
OUTPUTDIR=-o ./

.DELETE_ON_ERROR:

run: release
	dotnet ./RunZH.dll

release:
	$(RUNTIME) $(OUTPUTDIR)

debug:
	$(RUNTIME) $(OUTPUTDIR)

clean:
	rm RunZH.deps.json
	rm RunZH.dll
	rm RunZH.pdb
	rm RunZH.runtimeconfig.dev.json
	rm RunZH.runtimeconfig.json
