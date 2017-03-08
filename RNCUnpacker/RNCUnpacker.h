// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the RNCUNPACKER_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// RNCUNPACKER_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef RNCUNPACKER_EXPORTS
#define RNCUNPACKER_API __declspec(dllexport)
#else
#define RNCUNPACKER_API __declspec(dllimport)
#endif

#include "rnc.h"

EXTERN_C RNCUNPACKER_API rnc_status unpack(const uint8_t* input, uint8_t* output)
{
	return rnc_unpack(input, output);
}