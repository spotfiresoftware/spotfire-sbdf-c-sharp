/*
Copyright © 2024. Cloud Software Group, Inc.
This file is subject to the license terms contained
in the license file that is distributed with this file.
*/
namespace Spotfire.Sbdf.Samples
{
    using System;
    using System.IO;
    using Spotfire.Dxp.Data.Formats.Sbdf;

    /// <summary>
    /// This example is a simple command line tool that writes a simple SBDF file
    /// with random data.
    /// </summary>
    public class WriterSample
    {
        public static void Main(string[] args)
        {
            // The command line application requires one argument which is supposed to be
            // the name of the SBDF file to generate.
            if (args.Length != 1)
            {
                System.Console.WriteLine("Syntax: WriterSample outputfile.sbdf");
                return;
            }

            var outputFile = args[0];

            // First we just open the file as usual and then we need to wrap the stream
            // in a binary writer.
            using (var stream = File.OpenWrite(outputFile))
            using (var writer = new BinaryWriter(stream))
            {
                // When writing an SBDF file you first need to write the file header.
                SbdfFileHeader.WriteCurrentVersion(writer);

                // The second part of the SBDF file is the metadata, in order to create
                // the table metadata we need to use the builder class.
                var tableMetadataBuilder = new SbdfTableMetadataBuilder();

                // The table can have metadata properties defined. Here we add a custom
                // property indicating the producer of the file. This will be imported as
                // a table property in Spotfire.
                tableMetadataBuilder.AddProperty("GeneratedBy", "WriterSample.exe");

                // All columns in the table need to be defined and added to the metadata builder,
                // the required information is the name of the column and the data type.
                var col1 = new SbdfColumnMetadata("Category", SbdfValueType.String);
                tableMetadataBuilder.AddColumn(col1);

                // Similar to tables, columns can also have metadata properties defined. Here
                // we add another custom property. This will be imported as a column property
                // in Spotfire.
                col1.AddProperty("SampleProperty", "col1");

                var col2 = new SbdfColumnMetadata("Value", SbdfValueType.Double);
                tableMetadataBuilder.AddColumn(col2);
                col2.AddProperty("SampleProperty", "col2");

                var col3 = new SbdfColumnMetadata("TimeStamp", SbdfValueType.DateTime);
                tableMetadataBuilder.AddColumn(col3);
                col3.AddProperty("SampleProperty", "col3");

                // We need to call the build function in order to get an object that we can
                // write to the file.
                var tableMetadata = tableMetadataBuilder.Build();
                tableMetadata.Write(writer);

                const int rowCount = 10000;
                var random = new Random((int)DateTime.Now.Ticks);

                // Now that we have written all the metadata we can start writing the actual data.
                // Here we use a SbdfTableWriter to write the data, remember to dispose the table writer
                // otherwise you will not generate a correct SBDF file.
                using (var tableWriter = new SbdfTableWriter(writer, tableMetadata))
                {
                    for (int i = 0; i < rowCount; ++i)
                    {
                        // You need to perform one AddValue call for each column, for each row in the
                        // same order as you added the columns to the table metadata object.
                        // In this example we just generate some random values of the appropriate types.
                        // Here we write the first string column.
                        var col1Values = new[] {"A", "B", "C", "D", "E"};
                        tableWriter.AddValue(col1Values[random.Next(0, 5)]);

                        // Next we write the second double column.
                        var doubleValue = random.NextDouble();
                        if (doubleValue < 0.5)
                        {
                            // Note that if you want to write a null value you shouldn't send null to
                            // AddValue, instead you should use the InvalidValue property of the columns
                            // SbdfValueType.
                            tableWriter.AddValue(SbdfValueType.Double.InvalidValue);
                        }
                        else
                        {
                            tableWriter.AddValue(random.NextDouble());
                        }

                        // And finally the third date time column.
                        tableWriter.AddValue(DateTime.Now);
                    }
                }
            }

            System.Console.WriteLine("Wrote file: " + outputFile);
        }
    }
}
