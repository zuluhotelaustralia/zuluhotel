#include <stdio.h>

#define SPACESHOULDBE 30


/*
reads the
#define DEF ID
pattern and sets a space of SPACESHOULDBE bytes
between the start of DEF and the start of ID
used in UOProtocol.h ;)

the target file should have ONLY this format
*/
int main(int args, char **arg)
{
    FILE *Text = NULL;
    FILE *Targ = NULL;
    int i = 0, spacebetween = 0;
    
    char def[4096], id[4096], hex[4096];
    
    Text = fopen("Formated.h", "wb");
    Targ = fopen("UOProtocol.h", "rb");
    
    if(Targ == NULL)
    {
        printf("Could not open %s for read\n", arg[1]);
        system("pause");
        fclose(Text);
        return 0;
    }
    
    rewind(Targ);
    rewind(Text);
    
    while(fscanf(Targ, "%s %s %s", def, id, hex) != EOF)
    {
        printf(".");    
        spacebetween = SPACESHOULDBE - strlen(id);
        if(spacebetween < 1)
            spacebetween = 1;
        
        fprintf(Text, "%s %s", def, id);
        
        for(i = 0; i < spacebetween; i++)
            fprintf(Text, " ");
            
        fprintf(Text, "%s\r\n", hex);
        
        fflush(Text);
    }
    
    fclose(Targ);    
    fclose(Text);
    
    printf("\n");
    system("pause");
    
    return 1;
}
