#!/bin/sh

cd $PWD/$(dirname $0)

node foam/tools/foam.js --classpath=$PWD/js ca.zuluhotel.GenOres itemsPath=Server/Scripts/Items/Resources/Blacksmithing/Generated baseOre=Server/Scripts/Items/Resources/Blacksmithing/Generated/BaseOre.cs miningResources=Server/Scripts/Engines/Harvest/Generated/MiningResources.cs resourceInfo=Server/Scripts/Misc/Generated/ResourceInfo.cs defBlacksmithy=Server/Scripts/Engines/Craft/Generated/DefBlacksmithy.cs defTinkering=Server/Scripts/Engines/Craft/Generated/DefTinkering.cs resmelt=Server/Scripts/Engines/Craft/Core/Generated/Resmelt.cs
