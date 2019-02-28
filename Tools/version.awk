BEGIN {
    #run this on $ date +%y.%m.%d.%H as input
    outfile = "Server/Version.cs";
    FS = "."
}
{
    rev = (int($3) * 3) + int($4);
    print "//Generated File.  Do not modify by hand." > outfile;
    print "using System;" > outfile;
    print "using Server;" > outfile;
    print "namespace Server {" > outfile;
    print " public static class ServerVersion { " > outfile;
    print "  public const int Major = "int($1)";" > outfile;
    print "  public const int Minor = "int($2)";" > outfile;
    print "  public const int Rev = "rev";" > outfile;
    print " }" > outfile;
    print "}" > outfile;
}
