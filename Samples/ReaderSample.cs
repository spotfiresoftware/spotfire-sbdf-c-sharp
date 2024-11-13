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
    /// This example is a simple command line tool that reads an SBDF file
    /// and writes displays the content in the console.
    /// </summary>
    public class ReaderSample
    {
        public static void Main(string[] args)
        {
            // The command line application requires one argument which is supposed to be
            // the name of the SBDF file to read.
            if (args.Length != 1)
            {
                System.Console.WriteLine("Syntax: ReaderSample inputfile.sbdf");
                return;
            }

            var inputFile = args[0];

            // First we just open the file as usual and then we need to wrap the stream
            // in a binary reader.
            using (var stream = File.OpenRead(inputFile))
            using (var reader = new BinaryReader(stream))
            {
                // We need to start with reading the file header.
                SbdfFileHeader.Read(reader);

                // Next we need to read the table metadata.
                var tableMetadata = SbdfTableMetadata.Read(reader);

                // Write all table metadata. This is what you would see as table properties in Spotfire.
                foreach (var property in tableMetadata)
                {
                    Console.WriteLine("Table Property \"{0}\" (type {1}), value = \"{2}\"", property.Name, property.ValueType, property.Value);
                }

                // From the table metadata we can also recieve metadata about the columns in the table.
                var columns = tableMetadata.Columns;
                foreach (var column in columns)
                {
                    // All columns always have a name and a data type.
                    Console.WriteLine("Column: {0}, type: {1}", column.Name, column.DataType);

                    // Optionally a column may have additional metadata. This is what you will see
                    // as column properties in Spotfire. While you can iterate directly over all
                    // properties of the column metadata in this case we use the AssignedProperties
                    // property instead since that one ignores name and data type which we have 
                    // already written.
                    foreach (var property in column.AssignedProperties)
                    {
                        Console.WriteLine("Column Property \"{0}\" (type {1}), value = \"{2}\"", property.Name, property.ValueType, property.Value);
                    }
                }

                // Now we can read the data using a table reader. Since the API has a single ReadValue
                // call which reads the next cell we need to keep track of the current column/row index.
                int rowIndex = 0;
                int columnIndex = 0;
                using (var tableReader = new SbdfTableReader(reader, tableMetadata))
                {
                    while (true)
                    {
                        // Read the next cell.
                        var value = tableReader.ReadValue();
                        if (value == null)
                        {
                            // The read value call returns null when we have reached end of file.
                            break;
                        }

                        // Get the metadata for the current column.
                        var column = columns[columnIndex];

                        // Since null is indicating end of file we instead need to use the InvalidValue
                        // for the columns data type to check for null values.
                        if (value == column.DataType.InvalidValue)
                        {
                            Console.Write("(null);");
                        }
                        else
                        {
                            Console.Write("{0};", value);
                        }

                        columnIndex++;
                        if (columnIndex == columns.Count)
                        {
                            // We've reached the last column so this is the end of a row, start
                            // over with the first column again.
                            Console.WriteLine();
                            columnIndex = 0;
                            rowIndex++;
                        }
                    }
                }

                Console.WriteLine("{0} rows read.", rowIndex);
            }
        }
    }
}
