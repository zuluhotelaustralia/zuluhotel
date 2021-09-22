package main

/*
#include <stdint.h>
#include <stdlib.h>

struct foobar {
    int64_t A;
    char*   B;
};

static char**makeCharArray(int size) {
        return calloc(sizeof(char*), size);
}

static void setArrayString(char **a, char *s, int n) {
        a[n] = s;
}

static void freeCharArray(char **a, int size) {
        int i;
        for (i = 0; i < size; i++)
                free(a[i]);
        free(a);
}

*/
import "C"
import (
	"unsafe"
	"fmt"
	"cuelang.org/go/cue/cuecontext"
	"cuelang.org/go/cue/load"
	"runtime"
)

//export Compile
func Compile(contextDir *C.char, files *C.char, filesLen int) **C.char {
    filesSlice := (*[1<<28 - 1]*C.char)(unsafe.Pointer(files))[:filesLen:filesLen]
    
    entries := make([]string, filesLen)
    
    for idx, strPtr := range filesSlice {
        entries[idx] = C.GoString(strPtr)
    }
    fmt.Println("Received files:", entries)
   
	// We need a cue.Context, the New'd return is ready to use
	ctx := cuecontext.New()

	// Load Cue files into Cue build.Instances slice
    loadCfg := &load.Config {
        Dir: C.GoString(contextDir),
    }
	bis := load.Instances(entries, loadCfg)
	
	output := make([]string, 0, filesLen)

	// Loop over the instances, checking for errors and printing
	for _, bi := range bis {
		// check for errors on the instance
		// these are typically parsing errors
		if bi.Err != nil {
			fmt.Println("Error during load:", bi.Err)
			continue
		}

		// Use cue.Context to turn build.Instance to cue.Instance
		value := ctx.BuildInstance(bi)
		if value.Err() != nil {
			fmt.Println("Error during build:", value.Err())
			continue
		}

		// print the error
		// Validate the value
		err := value.Validate()
		if err != nil {
			fmt.Println("Error during validate:", err)
			continue
		}
		
		byts, err := value.MarshalJSON()
		if err != nil {
            fmt.Println("Error during JSON marshal:", err)
            continue
		}
		
		output = append(output, string(byts))
	}

    stringArray := C.makeCharArray(C.int(len(output)))
    for i, s := range output {
        C.setArrayString(stringArray, C.CString(s), C.int(i))
    }
    
//     defer C.freeCharArray(stringArray, C.int(len(entries)))

    output = nil
    ctx = nil
    bis = nil
    
    runtime.GC()
	
	return stringArray
}


//export Free
func Free(array **C.char, len int) {
    fmt.Println("Trying to free:", array)
//     C.freeCharArray(array, C.int(len))
    runtime.GC()
}

func main() {
}