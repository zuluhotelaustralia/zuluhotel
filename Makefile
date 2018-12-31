RUNTIME=dotnet build
OUTPUTDIR=-o ./

.DELETE_ON_ERROR:

release:
	$(RUNTIME) $(OUTPUTDIR)

run: release
	dotnet ./RunZH.dll

debug:
	$(RUNTIME) $(OUTPUTDIR)

clean:
	rm RunZH.deps.json
	rm RunZH.dll
	rm RunZH.pdb
	rm RunZH.runtimeconfig.dev.json
	rm RunZH.runtimeconfig.json
