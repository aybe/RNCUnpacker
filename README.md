# RNCUnpacker
Rob Northern Compression unpacker for .NET

This is a C# wrapper around RNC implementation found in [CorsixTH](https://github.com/CorsixTH/CorsixTH).

I didn't reinvent the wheel (actually I tried but failed miserably), the assembly wraps the native implementation in a friendly ```RNCStream``` type which, if underlying data is RNC-compressed will be uncompressed first else it will be passed through.

##Licence

TBD
