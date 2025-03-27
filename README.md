# C# Library for Spotfire® Binary Data Format (SBDF)

## Product Overview 

The Spotfire C# SBDF Library is a .NET library for reading and writing files in the Spotfire Binary Data File (SBDF) format.\
The library can be used to add support for this format to an application, enabling it to natively create and consume data in a way optimized for analysis in Spotfire Analyst, TERR and Metrics.
This package also contains two simple examples of how the library can be used.

## Benefits
- Enables Spotfire customers to integrate Spotfire directly into their data workflows and business processes, by generating and consuming data in an optimal format for Spotfire.
- Freely redistribute this library with your 3rd-party applications, giving your users a streamlined experience when working with your applications and Spotfire, enabling them to easily take advantage of the strengths of both.
- Pass data between your application and the Spotfire platform in a highly-efficient binary format, increasing performance and preserving data type information. _(Even though Spotfire products can read data on a number of different formats, SBDF files can increase the performance when using the data in a Spotfire analysis.)_

### Contents:

| File | Description |
| ------ | ------ |
| README.MD | This file. |
| LICENSE | The license. |
| Spotfire.Dxp.Data.Formats.Sbdf.dll |  The .NET library for reading/writing SBDF files. | 
| Spotfire.Dxp.Data.Forms.Sbdf.xml |  An XML file that provides support for intellisense in Visual Studio. | 
| Samples |  Contains simple examples of basic functionality that use the SBDF library. | 
| Samples/ReaderSample.cs |  An example program that reads an SBDF file and writes out the content to the console. | 
| Samples/WriterSample.cs |  An example program that creates an SBDF file. | 
| Samples/Makefile |  A Makefile that can be used by running nmake from Visual Studio in order build the samples. | 


This is a .NET library for reading and writing Spotfire Binary Data Files
(SBDF) files. You can use this library to add support for SBDF files to your
application. 

>_Note: If you are writing an extension to Spotfire you should not use this
DLL. Instead, use the DLL provided by the Spotfire SDK._


### Documentation:

Documentation on the SBDF library can be found [here](https://docs.tibco.com/pub/doc_remote/spotfire/6.5.0/api/?topic=html/N_Spotfire_Dxp_Data_Formats_Sbdf.htm)


## License

BSD-type 3-Clause License. See the file LICENSE included in the repository.
