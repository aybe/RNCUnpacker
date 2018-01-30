# RNCUnpacker

Rob Northern Compression unpacker for .NET

This is a C# library that wraps the native implementation found in [CorsixTH](https://github.com/CorsixTH/CorsixTH). It is exposed as an ```RNCStream``` which will automatically unpack packed content if it is, e.g. you can use it to load any content transparently, whether or not it is RNC-packed.

### Download

See the [releases tab](https://github.com/aybe/RNCUnpacker/releases).

There are two flavors available:

- regular .NET 4.6 assemblies 
- Unity3D .NET 4.6 assemblies
 
(they only differ by their ```DLLImport``` attributes)

### Known issues

The native implementation only supports the first compression scheme.