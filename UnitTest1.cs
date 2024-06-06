using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System.Reflection.PortableExecutable;
using TestProject_1;
using static TestProject_1.ProgrammLab1;

namespace TestProject_1_unitests
{
    public class Tests
    {
        private ProgrammLab1 program;

        [SetUp]
        public void Setup()
        {
            program = new ProgrammLab1();

        }

        [Test]
        public void Test_IsInAlphabet1()
        {
            char[] consonantsAlphabet = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z',
                                      'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };


            bool result = program.IsInAlphabet('a', consonantsAlphabet);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_IsInAlphabet2()
        {
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


            bool result = program.IsInAlphabet('a', alphabet);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_ReadFile()
        {
            StringWriter output = new StringWriter();
            string noFile = "nothing.txt";

            StreamReader reader = program.ReadFile(noFile, output);

            Assert.That(output.ToString(), Does.Contain("File can't be opened"));
            Assert.That(reader, Is.Null);
        }



        [TestCase("Hello", 2)]
        [TestCase("spray", 3)]
        [TestCase("charter", 2)]
        [TestCase("fraction", 2)]
        [TestCase("adoption", 2)]
        [TestCase("detail", 1)]
        [TestCase("consideration", 2)]
        public void Test_NumOfConsonants(string inputContext, int expected)
        {
            Word input = new Word { Size = inputContext.Length, Context = inputContext };

            Assert.AreEqual(expected, program.NumOfConsonants(input));
        }



        /*private static IEnumerable<TestCaseData> TextCases_NumOfConsonants()
        {
            yield return new TestCaseData("Hello", 2);
            yield return new TestCaseData("spray", 3);
            yield return new TestCaseData("charter", 2);
            yield return new TestCaseData("fraction", 2);
            yield return new TestCaseData("adoption", 2);
            yield return new TestCaseData("detail", 1);
            yield return new TestCaseData("consideration", 2);

        }

        [Test, TestCaseSource(nameof(TextCases_NumOfConsonants))]
        public void Test_NumOfConsonants(string inputContext, int expected)
        {
            Word input = new Word { Size = inputContext.Length, Context = inputContext };
            Assert.That(program.NumOfConsonants(input), Is.EqualTo(expected));
        }*/

        [Test]
        public void Test_Main()
        {
            string testFile = "E:\\Labs\\Labs_Metods\\Lab1_VS\\TestProject_1\\TestProject_1\\TextFile1.txt";

            List<string> output = program.MainFunction(testFile);

            Console.WriteLine(output.ToString());

            CollectionAssert.AreEquivalent(output, new List<string> { "ultrices", "ultricies", "porttitor" });
        }
    }
}