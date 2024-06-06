using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_1
{
    public class ProgrammLab1
    {
        public bool IsInAlphabet(char a, char[] alphabet)
        {
            foreach (char c in alphabet)
            {
                if (a == c)
                    return true;
            }
            return false;
        }

        public struct Word
        {
            public int Size;
            public string Context;
        }

        public int NumOfConsonants(Word a)
        {
            char[] consonantsAlphabet = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z',
                                      'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };

            int res = 0;
            int temp = 0;

            foreach (char c in a.Context)
            {
                if (IsInAlphabet(c, consonantsAlphabet))
                {
                    temp++;
                    if (temp > res)
                        res = temp;
                }
                else
                    temp = 0;
            }

            return res;
        }

        public bool IfWordsSimilar(Word a, Word b)
        {
            if (a.Size != b.Size)
                return false;

            return a.Context.Equals(b.Context);
        }

        public StreamReader ReadFile(string filename, StringWriter output)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filename);
            }
            catch (IOException e)
            {
                output.WriteLine("File can't be opened: " + e.Message);
            }

            return reader;
        }


        public List<string> MainFunction(string inputFile)
        {
            var output = new List<string>();
            StringWriter log = new StringWriter();
            StreamReader reader = ReadFile(inputFile, log);
            Console.WriteLine(log.ToString());

            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            var text = new Word[100];
            int numWords = 0;
            text[0].Size = 0;
            text[0].Context = "";

            try
            {
                char ch;
                do
                {
                    ch = (char)reader.Read();
                    if (IsInAlphabet(ch, alphabet))
                    {
                        text[numWords].Size++;
                        text[numWords].Context += ch;
                        ch = (char)reader.Read();
                        while (IsInAlphabet(ch, alphabet))
                        {
                            text[numWords].Size++;
                            text[numWords].Context += ch;
                            ch = (char)reader.Read();
                        }
                        numWords++;
                        text[numWords].Size = 0;
                        text[numWords].Context = "";

                        if ((numWords % 100) == 99)
                        {
                            Array.Resize(ref text, numWords + 101);
                        }
                    }
                } while (reader.Peek() != -1);
            }
            finally
            {
                reader.Close();
            }

            int max = NumOfConsonants(text[0]);
            int[] listMax = new int[1];
            listMax[0] = 0;
            int sizeListMax = 1;

            for (int i = 1; i < numWords; i++)
            {
                int numConsonants = NumOfConsonants(text[i]);
                if (numConsonants > max)
                {
                    Array.Clear(listMax, 0, listMax.Length);
                    listMax[0] = i;
                    sizeListMax = 1;
                    max = numConsonants;
                }
                else if (numConsonants == max)
                {
                    Array.Resize(ref listMax, sizeListMax + 1);
                    listMax[sizeListMax++] = i;
                }
            }

            for (int i = 0; i < sizeListMax; i++)
            {
                bool check = true;
                for (int j = 0; j < i; j++)
                {
                    if (IfWordsSimilar(text[listMax[i]], text[listMax[j]]))
                        check = false;
                }
                if (check)
                {
                    output.Add(text[listMax[i]].Context);
                }
            }

            return output;
        }
    }
}
