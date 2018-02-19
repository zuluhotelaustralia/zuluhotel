BEGIN{
    FS="|";
    RS="\n";
    OFS="";
    ORS="\n";

    counter = 0;
    outfile = "";
    resname="";
    restype="Granite";
    trimmedres="";
    print "Generating Granite classes...";
}

function newOutfile() {
    outfile=resname restype ".cs";
}

function doGsubs(){
    resname = $2;
    trimmedres = $2;
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
    print "\tpublic class "resname"Granite : BaseGranite {" > outfile;
    print "\t\t[Constructable]" > outfile;
    print "\t\tpublic "resname"Granite() : base( CraftResource."resname" ) {}" > outfile;
    print "" > outfile;
    print "\t\tpublic "resname"Granite( Serial serial ) : base( serial ) {}" > outfile;
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
    print "" > outfile;
    print "\t\tpublic override void OnSingleClick( Mobile from ) {" > outfile;
    print "\t\t\tfrom.SendMessage(\""trimmedres" " restype"\");" > outfile;
    print "\t\t}" > outfile;
    print "\t}	" > outfile;
    print "}" > outfile;

    close outfile;
    counter++;
}

END {
    print "Done.  Generated " counter " class files into a subdirectory.";
}