#pragma once

#ifdef RNCUNPACKER_EXPORTS
#define RNCUNPACKER_API __declspec(dllexport)
#else
#define RNCUNPACKER_API __declspec(dllimport)
#endif

#include "rnc.h"

EXTERN_C inline RNCUNPACKER_API rnc_status unpack(const uint8_t* input, uint8_t* output)
{
	return rnc_unpack(input, output);
}