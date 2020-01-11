ORESRC=../genores.awk
INGOTSRC=../geningots.awk
LOGSRC=../genlogs.awk
BOARDSRC=../genboards.awk
FISHSRC=../genfish.awk
ORENODES=../orelist.org
LOGNODES=../loglist.org
FISHNODES=../fishlist.org
GRANITESRC=../gengranite.awk
HIDESRC=../genhides.awk
HIDENODES=../hidelist.org
LEATHERSRC=../genleather.awk

all:
	help

help:
	@echo "Valid targets:"
	@echo "clean - Cleans up"
	@echo "granite - Generates granite class files"
	@echo "ores - Generates ore and ingot class files"
	@echo "logs - Generates log and board class files"
	@echo "fish - Not implemented yet"

ores:
	mkdir -p mining
	cd mining/; awk -f $(ORESRC) $(ORENODES); awk -f $(INGOTSRC) $(ORENODES)

logs:
	mkdir -p lumberjacking
	cd lumberjacking; awk -f $(LOGSRC) $(LOGNODES); awk -f $(BOARDSRC) $(LOGNODES)

fish:
	@echo "I know Help says this is a valid target but it's not done yet."

granite:
	mkdir -p granite
	cd granite/; awk -f $(GRANITESRC) $(ORENODES)

hides:
	mkdir -p hides
	cd hides/; awk -f $(HIDESRC) $(HIDENODES); awk -f $(LEATHERSRC) $(HIDENODES)

clean:
	rm -rf lumberjacking/
	rm -rf mining/
	rm -rf granite/
	rm -rf hides/
	rm -rf leather/
