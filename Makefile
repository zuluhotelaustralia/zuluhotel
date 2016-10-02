CC=mcs
LDFLAGS=-reference:System.Drawing
OPTFLAGS=-optimize+
CFLAGS=-d:MONO -d:NEWPARENT -d:NEWTIMERS -unsafe -nowarn:219,414 -t:exe -out:RunUO.exe
RECURSE=-recurse:'Server/*.cs'
DFLAGS=-debug

# mcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:MONO -recurse:Server/*.cs

all: release

help:
	@echo "Targets:"
	@echo "configure: creates client folder directory 'muls/' where the server expects to see client data."
	@echo "release:  compiles Server binary with optimization flags"
	@echo "debug:  compiles Server binary with debugging symbols"
	@echo "clean:  removes Server binary"

configure:
	mkdir muls
	@echo "Folder muls/ created, you should put a copy of the client files here."

release:
	$(CC) $(CFLAGS) $(OPTFLAGS) $(LDFLAGS) $(RECURSE)

debug:
	$(CC) $(CFLAGS) $(DFLAGS) $(LDFLAGS) $(RECURSE)

clean:
	rm RunUO.exe
