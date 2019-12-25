BEGIN{
    FS="|";
    RS="\n";
    OFS="";
    ORS="\n";

    counter = 0;
    outfile = "";
    resname="";
    restype="Ingot";
    trimmedres="";
    hue="";
    print "Generating Ingot classes...";
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
    print "// Generated File. DO NOT MODIFY BY HAND." > outfile;
    print "namespace Server.Items {" > outfile;
    print "" > outfile;
    print "\t[FlipableAttribute( 0x1BF2, 0x1BEF )]" > outfile;
    print "\tpublic class "resname restype" : BaseIngot {" > outfile;
    print "\t\t[Constructable]" > outfile;
    print "\t\tpublic "resname restype"() : this( 1 ) {}" > outfile;
    print "" > outfile;
    print "\t\t[Constructable]" > outfile;
    print "\t\tpublic "resname restype"( int amount ) : base( CraftResource."resname", amount ) {" > outfile;
    print "\t\t\tthis.Hue = " hue ";" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic "resname restype"( Serial serial ) : base( serial ) {}" > outfile;
    print "" > outfile;
    print "\t\tpublic override void Serialize( GenericWriter writer ) {" > outfile;
    print "\t\t\tbase.Serialize( writer );" > outfile;
    print "\t\t\twriter.Write( (int) 0 ); // version" > outfile;
    print "\t\t}" > outfile;
    print "" > outfile;
    print "\t\tpublic override void Deserialize( GenericReader reader ) {" > outfile;
    print "\t\t\tbase.Deserialize( reader );" > outfile;
    print "\t\t\tint version = reader.ReadInt();" > outfile;
    print "\t\t}" > outfile;
    print "\t}	" > outfile;
    print "}" > outfile;

    close(outfile);
    counter++;
}

END {
    print "Done.  Generated " counter " class files into a subdirectory.";
}
