function getMaxSkill() {
    minskill = $3;
    if (minskill+100 >= 150) {
	return 150;
    }
    else {
	return minskill+100;
    }
}

BEGIN {
    FS = ","; #field separator
    RS = "\n"; #record (line) separator
    OFS = ""; #output fs
    ORS = "\n"; #output rs

    print "[";
}

#all
{
    print "{";
    print "\"model_\": \"ca.zuluhotel.Ore\",";
    print "\"desc\": \"" $1 "\",";
    gsub (" ", "", $1);
    print "\"name\": \"" $1 "\",";
    print "\"maxSkill\": " getMaxSkill() ",";
    print "\"minskill\": " $3 ",";
    print "\"reqskill\": " $3 ",";
    print "\"veinChance\": " 0 ",";
    print "\"abundance\": " 50 ",";
    print "\"fallbackChance\": " 0 ",";
    print "},";
}

END {
    print "]";
}
