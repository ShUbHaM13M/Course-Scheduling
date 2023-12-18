using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekTrust
{
    public class Program
    {
        static readonly CourseScheduler courseScheduler = new();
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Error: Input file not provided");
                    Environment.Exit(1);
                }
                string fileName = args[0];
                var commands = CommandProcessor.ProcessFile(fileName);
                List<string> results = [];
                foreach (var command in commands)
                {
                    results.Add(courseScheduler.ProcessCommand(command));
                }
                Console.WriteLine(string.Join(Environment.NewLine, results));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
