using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Command = System.ValueTuple<string, System.Collections.Generic.Dictionary<string, string>>;

namespace GeekTrust
{
    public class CommandProcessor
    {
        public static readonly Dictionary<string, string[]> commands = new()
        {
            {"ADD-COURSE-OFFERING", ["COURSE-NAME", "INSTRUCTOR", "DATE", "MIN-EMPLOYEES", "MAX-EMPLOYEES"]},
            {"REGISTER", ["EMAIL-ID", "COURSE-OFFERING-ID"]},
            {"ALLOT", ["COURSE-OFFERING-ID"]},
            {"CANCEL", ["COURSE-REGISTERATION-ID"]},
        };

        static private Command ParseCommand(string command)
        {
            string[] values = command.Trim().Split(" ");
            command = values[0];
            var argValues = new ArraySegment<string>(values, 1, values.Length - 1);

            if (!commands.ContainsKey(command))
            {
                Console.Error.WriteLine($"Error: {command} Command not found");
                Environment.Exit(1);
            }

            commands.TryGetValue(command, out string[] argKeys);
            if (argKeys.Length != argValues.Count)
            {
                Console.Error.WriteLine($"Error: Not enough args passed for command {command}");
                Environment.Exit(1);
            }

            Dictionary<string, string> argMap = [];
            foreach (var (key, value) in argKeys.Zip(argValues))
                argMap.Add(key, value);

            return (command, argMap);
        }

        static public List<Command> ProcessFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.Error.WriteLine($"Error: File {fileName} does not exists");
                Environment.Exit(1);
            }

            List<Command> commands = [];
            using StreamReader sr = File.OpenText(fileName);
            string command;
            while ((command = sr.ReadLine()) != null)
            {
                if (command.StartsWith("//")) continue;
                commands.Add(ParseCommand(command));
            }

            return commands;
        }
    }
}