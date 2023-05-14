using System.Collections.Generic;

namespace KursovaRabotaSAA
{
    internal class StringConverter
    {
        //Подава нужния прой таблуации, искани при изрисуване на дървото
        public static string NumberOfTabluations(int tabIndex)
        {
            string tabs = null;
            for (int i = 0; i < tabIndex; i++)
            {
                tabs += "   ";
            }

            return tabs;
        }

        //Разделя команда чрез Slash в List
        public static List<string> SplitBySlash(string inputText)
        {
            List<string> answer = new List<string>();
            string word = null;

            for (int i = 2; i < inputText.Length; i++)
            {
                if (inputText[i] != '/')
                    word += inputText[i];

                else
                {
                    answer.Add(word);
                    word = null;
                }

                if (i == inputText.Length - 1)
                    answer.Add(word);
            }

            return answer;
        }

        //Разделя частите на подадена команда чрез таблуация
        public static string[] SplitBySpace(string inputText)
        {
            string[] answer = new string[2];
            string word = null;
            int i = 0, c = 0;

            do
            {
                if (inputText[c] != ' ')
                {
                    word += inputText[c];
                    if (c == inputText.Length - 1)
                    {
                        answer[i] = word;
                        word = null;
                        i++;
                    }
                }
                else
                {
                    answer[i] = word;
                    word = null;
                    i++;
                }
                c++;

            } while (c < inputText.Length);

            return answer;
        }

        //Порверява дали в командата от потребителя се съдържат квадратни скоби
        public static bool SquareBracketsChecker(string text)
        {
            bool more = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                    more = true;
            }
            return more;
        }

        //Разделя частите на подадена команда чрез квадратните скоби на Име на Node и аргумент на Node
        public static string[] SplitByBrackets(string text)
        {
            string[] result = new string[2];
            string tag = null;
            string path = "";

            for (int i = 0; i < text.Length;)
            {
                if (text[i] == '[')
                {
                    i++;
                    do
                    {
                        if (text[i] != '@')
                        {
                            path += text[i];
                        }
                        i++;
                    } while (text[i] != ']');

                    result[1] = path;
                    break;
                }
                else
                {
                    do
                    {
                        tag += text[i];
                        i++;
                    } while (text[i] != '[');
                    result[0] = tag;
                }
            }

            return result;
        }

        //Премахва първия char от string 
        public static string FirstCharEraser(string text)
        {
            string word = null;
            for (int i = 1; i < text.Length; i++)
            {
                word += text[i];
            }

            return word;

        }

        //Взима името на тага, от още нередактирания текст от HTML-файла
        public static string GetTheName(string text)
        {
            string word = "";

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] != ' ' && text[i] != '>')
                {
                    word += text[i];
                }
                else
                {
                    return word;
                }
            }

            return word;
        }
    }
}
