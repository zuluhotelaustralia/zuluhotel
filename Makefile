CC=mcs
OPTFLAGS=-optimize+
CFLAGS=-unsafe -nowarn:219,414 -out:Server.exe
RECURSE=-recurse:'Server/*.cs'
DFLAGS=-debug

.DELETE_ON_ERROR:

# mcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:MONO -recurse:Server/*.cs

.PHONY: client release debug clean

all: debug client

run: debug
	mono Server.exe

help:
	@echo "Targets:"
	@echo "configure: creates client folder directory 'muls/' where the server expects to see client data."
	@echo "release:  compiles Server binary with optimization flags"
	@echo "debug:  compiles Server binary with debugging symbols"
	@echo "clean:  removes Server binary"

configure:
	mkdir muls
	@echo "Folder muls/ created, you should put a copy of the client files here."

client: Client/Cliloc.enu

Client/Cliloc.enu: Client/clilocs_enu.scm
	guile -L uotools -c "(use-modules (uo cliloc)) (compile-clilocs (current-input-port) (current-output-port))" < $< > $@

release:
	$(CC) $(CFLAGS) $(OPTFLAGS) $(LDFLAGS) $(RECURSE)

debug:
	$(CC) $(CFLAGS) $(DFLAGS) $(LDFLAGS) $(RECURSE)

clean:
	rm Server.exe
