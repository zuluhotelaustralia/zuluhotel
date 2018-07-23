RUNTIME=dotnet build
FRAMEWORK=-f netcoreapp2.1
CONFIGURATION=-c Release
OUTPUTDIR=-o ../

.DELETE_ON_ERROR:


run: release
	mono Server.exe

client: Client/Cliloc.enu

Client/Cliloc.enu: Client/clilocs_enu.scm
	guile -L Tools/uotools -c "(use-modules (uo cliloc)) (compile-clilocs (current-input-port) (current-output-port))" < $< > $@

release:
	$(RUNTIME) $(FRAMEWORK) $(CONFIGURATION) $(OUTPUTDIR)

debug:
	$(RUNTIME) $(FRAMEWORK) $(OUTPUTDIR)

clean:
	rm Server.dll
	rm Server.pdb
	rm Server.deps.json
	rm Server.runtimeconfig.json
	rm Server.runtimeconfig.dev.json
