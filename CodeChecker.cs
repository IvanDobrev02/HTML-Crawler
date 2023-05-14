using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaRabotaSAA
{
    internal class CodeChecker
    {
        public static string[] tagException = new string[] { "br", "col", "hr", "img", "input", "link", "meta", "param" };
        public static void Test(string content)
        {
            List<string> checkElements = new List<string>();
            HTMLStack stack = new HTMLStack();

            string word = null;
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '<')
                {
                    i++;

                    if (content[i] == ' ')
                    {
                        Console.WriteLine("There is space somewhere before a tag in your text!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

                    while (content[i] != ' ' && content[i] != '>')
                    {
                        word += content[i];
                        i++;
                    }

                    bool exist = false;
                    for (int t = 0; t < tagException.Length; t++)
                    {
                        if (word == tagException[t])
                            exist = true;
                    }

                    if (!exist)
                        checkElements.Add(word);

                    word = null;
                }
            }

            stack.Push(checkElements[0]);
            for (int i = 1; i < checkElements.Count; i++)
            {
                if (checkElements[i][0] == '/')
                    checkElements[i] = StringConverter.FirstCharEraser(checkElements[i]);

                if (checkElements[i] == stack.Peek())
                    stack.Pop();
                else
                    stack.Push(checkElements[i]);
            }

            if (stack.IsEmpty())
            {
                Console.WriteLine("Your file is OK!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Something is wrong with your file's text!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}