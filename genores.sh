#!/bin/sh

cd $PWD/$(dirname $0)

node foam/tools/foam.js --classpath=$PWD/js ca.zuluhotel.GenOres itemsPath=Scripts/Items/Resources/Blacksmithing/Generated baseOre=Scripts/Items/Resources/Blacksmithing/Generated/BaseOre.cs miningResources=Scripts/Engines/Harvest/Generated/MiningResources.cs resourceInfo=Scripts/Misc/Generated/ResourceInfo.cs
