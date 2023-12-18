using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace GeekTrust.Tests
{


    [TestFixture]
    public class ProgramTests
    {

        private readonly string ROOT_DIR = "../../../../GeekTrust/";

        private void TestFile(string fileName, string output)
        {
            string input = ROOT_DIR + fileName;
            var commands = CommandProcessor.ProcessFile(input);
            List<string> results = [];

            foreach (var command in commands)
                results.Add(Program.ProcessCommand(command));

            string result = string.Join(Environment.NewLine, results);
            Assert.AreEqual(result, output, $"Output of {fileName} does not match");
        }

        [Test]
        public void TestProcessingFile1()
        {
            string filePath = "sample_input/input1.txt";
            string output = @"OFFERING-JAVA-JAMES
OFFERING-KUBERNETES-WOODY
REG-COURSE-shubham-JAVA ACCEPTED
REG-COURSE-ANDY-JAVA ACCEPTED
REG-COURSE-ANDY-JAVA ANDY@GMAIL.COM OFFERING-JAVA-JAMES JAVA ANDY 15062022 CONFIRMED
REG-COURSE-shubham-JAVA shubham@mail.com OFFERING-JAVA-JAMES JAVA shubham 15062022 CONFIRMED";
            TestFile(filePath, output);
        }

        [Test]
        public void TestProcessingFile2()
        {
            string filePath = "sample_input/input2.txt";
            string output = @"OFFERING-DATASCIENCE-BOB
REG-COURSE-WOO-DATASCIENCE ACCEPTED
REG-COURSE-ANDY-DATASCIENCE ACCEPTED
REG-COURSE-ANDY-DATASCIENCE ANDY@GMAIL.COM OFFERING-DATASCIENCE-BOB DATASCIENCE ANDY 05062022 CONFIRMED
REG-COURSE-WOO-DATASCIENCE WOO@GMAIL.COM OFFERING-DATASCIENCE-BOB DATASCIENCE WOO 05062022 CONFIRMED";
            TestFile(filePath, output);
        }

        [Test]
        public void TestProcessingFile3()
        {
            string filePath = "sample_input/input3.txt";
            string output = @"OFFERING-PYTHON-JOHN
REG-COURSE-WOO-PYTHON ACCEPTED
REG-COURSE-ANDY-PYTHON ACCEPTED
REG-COURSE-BOBY-PYTHON ACCEPTED
REG-COURSE-BOBY-PYTHON CANCEL_ACCEPTED
REG-COURSE-ANDY-PYTHON ANDY@GMAIL.COM OFFERING-PYTHON-JOHN PYTHON ANDY 05062022 CONFIRMED
REG-COURSE-WOO-PYTHON WOO@GMAIL.COM OFFERING-PYTHON-JOHN PYTHON WOO 05062022 CONFIRMED";
            TestFile(filePath, output);
        }

    }
}
