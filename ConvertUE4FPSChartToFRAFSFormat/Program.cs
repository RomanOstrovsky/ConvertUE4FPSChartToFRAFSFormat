using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConvertUE4FPSChartToFRAFSFormat
{
    class Program
    {
        static void Main(string[] args)
        { 
            if (args.Length > 0 && File.Exists(args[0]))
            {
                const string STARTING_LINE = "Frame, Time (ms)";
                string SourceFile = args[0];
                string DestinationFile = Path.GetFileNameWithoutExtension(SourceFile) + "Converted.csv";
                string directory = Path.GetDirectoryName(SourceFile);
                string newFilePath = Path.Combine(directory, DestinationFile);

                string[] oldFileContents = File.ReadAllLines(SourceFile);

                int frame = 1;
                double time = 0.000;
                string newLine = string.Empty;
                int FirstComma = 0;
                int SecondComma = 0;
                int NumberLength = 0;

                using (StreamWriter newFile = new StreamWriter(newFilePath))
                {
                    newFile.WriteLine(STARTING_LINE);
                    for (int Line = 5; Line < oldFileContents.Length; Line++)
                    {
                        newLine = frame.ToString() + ',' + time.ToString("F3");
                        newFile.WriteLine(newLine);
                        FirstComma = oldFileContents[Line].IndexOf(',');
                        SecondComma = oldFileContents[Line].IndexOf(',', FirstComma + 1);
                        NumberLength = SecondComma - FirstComma - 1;
                        time += Convert.ToDouble(oldFileContents[Line].Substring(FirstComma + 1, NumberLength));
                        frame++;
                    }
                }
            }
        }
    }
}
