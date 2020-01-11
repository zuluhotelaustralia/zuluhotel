BEGIN {
    FS="|";
    RS="\n";
    OFS="";
    ORS="\n";

    counter = 0;
    outfile = "";
    resname="";
    restype="Hide";
    trimmedres="";
    print "Generating Hide classes...";
}

function newOutfile() {
    outfile=resname restype ".cs";
}

function doGsubs(){
    resname = $2;
    trimmedres = $2;
    hue = $3;
    quality = $5;
    gsub(/ /, "", quality);
    gsub(/ /, "", hue);
    gsub(/ /, "", resname);
    gsub(/^[ \t]+/, "", trimmedres);
    gsub(/[ \t]+$/, "", trimmedres);
}

{
    doGsubs();
    newOutfile();

    print "//Generated file.  Do not modify by hand." > outfile;
    print "namespace Server.Items{" > outfile;
    print "\t[FlipableAttribute( 0x1079, 0x1078 )]" > outfile;
    print "\tpublic class " resname restype" : BaseHides, IScissorable" > outfile;
    print "\t{" > outfile;
    print "\t\t[Constructable]" > outfile;
    print "\t\tpublic "resname restype"() : this( 1 )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\t[Constructable]" > outfile;
    print "\t\tpublic "resname restype"( int amount ) : base( CraftResource."resname"Leather, amount )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t\tthis.Hue = " hue ";" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic "resname restype"( Serial serial ) : base( serial )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic override void Serialize( GenericWriter writer )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t\tbase.Serialize( writer );" > outfile;
    print "" > outfile;
    print "\t\t\twriter.Write( (int) 0 ); // version" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic override void Deserialize( GenericReader reader )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t\tbase.Deserialize( reader );" > outfile;
    print "" > outfile;
    print "\t\t\tint version = reader.ReadInt();" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic bool Scissor( Mobile from, Scissors scissors )" > outfile;
    print "\t\t{" > outfile;
    print "\t\t\tif ( Deleted || !from.CanSee( this ) ) return false;" > outfile;
    print "" > outfile;
    print "\t\t\tif ( Core.AOS && !IsChildOf ( from.Backpack ) )" > outfile;
    print "\t\t\t{" > outfile;
    print "\t\t\t\tfrom.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack" > outfile;
    print "\t\t\t\treturn false;" > outfile;
    print "\t\t\t}" > outfile;
    print "\t\t\tbase.ScissorHelper( from, new "resname"Leather(), 1 );" > outfile;
    print "" > outfile;
    print "\t\t\treturn true;" > outfile;
    print "\t\t}" > outfile;
    print "\t}" > outfile;
    print "}" > outfile;
    
    close(outfile);
    counter++;
}

END {
    print "Done.  Generated " counter " class files into a subdirectory.";
}
