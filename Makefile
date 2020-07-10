RUNTIME=dotnet build
OUTPUTDIR=-o ./bin

.DELETE_ON_ERROR:

release:
	$(RUNTIME) -c Release $(OUTPUTDIR)/Release

run: release
	dotnet run $(OUTPUTDIR)/RunZH

debug:
	$(RUNTIME) -c Debug $(OUTPUTDIR)/Debug

clean:
	-rm $(OUTPUTDIR)
