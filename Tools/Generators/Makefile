ORESRC=../genores.awk
INGOTSRC=../geningots.awk
LOGSRC=../genlogs.awk
BOARDSRC=../genboards.awk
FISHSRC=../genfish.awk
ORENODES=../orelist.org
LOGNODES=../loglist.org
FISHNODES=../fishlist.org

all:
	help

help:
	@echo "Valid targets:"
	@echo "clean - Cleans up"
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

clean:
	rm -rf lumberjacking/
	rm -rf mining/
