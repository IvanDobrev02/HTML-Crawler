using System.Collections.Generic;

namespace KursovaRabotaSAA
{
    internal class TreeMaker
    {

        //Отделя Node-овете от текста на файлът
        public static List<string> ElementSplitter(string textFromFile)
        {
            List<string> words = new List<string>();
            string word = null;

            for (int i = 0; i < textFromFile.Length - 1; i++)
            {
                if (textFromFile[i] == '<')
                {
                    do
                    {
                        word += textFromFile[i];
                        i++;
                    }
                    while (textFromFile[i] != '>');
                    word += '>';
                    words.Add(word);
                    word = null;
                }

                if (textFromFile[i] != '>' && textFromFile[i] != '\t' && textFromFile[i] != ' ' && textFromFile[i] != '\n' && textFromFile[i] != '\r')
                {
                    do
                    {
                        word += textFromFile[i];
                        i++;
                    }
                    while (textFromFile[i] != '<' && textFromFile[i] != '\t' && textFromFile[i] != '\n' && textFromFile[i] != '\r');
                    i--;
                    words.Add(word);
                    word = null;
                }

            }

            return words;
        }

        //От вече отделените Node-ове изгражда самото дърво
        public static void TreeBuilding(List<string> elements, ref HTMLNode currNode)
        {
            for (int i = 1; i < elements.Count; i++)
            {
                for (int c = 0; c < elements[i].Length; c++)
                {
                    if (elements[i][c] == '<')
                    {
                        if (elements[i][c + 1] == '/')
                        {
                            if (elements[i] != "</html>")
                                currNode = currNode.Parent;
                            else
                                return;
                        }
                        else
                        {
                            HTMLNode child = new HTMLNode(elements[i], "Tag", currNode.NumberOfParents + 1, null, currNode, new List<HTMLNode>(), 1);
                            currNode.ChildrenList.Add(child);

                            bool except = true;
                            string a = StringConverter.GetTheName(child.TagName);
                            for (int g = 0; g < CodeChecker.tagException.Length; g++)
                            {
                                if (a == CodeChecker.tagException[g])
                                {
                                    except = false;
                                    child.Type = "SCTag";
                                }
                            }
                            if (except)
                                currNode = child;
                        }
                    }
                    else
                    {
                        HTMLNode child = new HTMLNode(elements[i], "Value", currNode.NumberOfParents + 1, null, currNode, new List<HTMLNode>(), 0);
                        currNode.ChildrenList.Add(child);
                    }

                    c = elements[i].Length - 1;
                }
            }

        }

        //Разделя името на тага и аргумента
        public static void ArgumentSplitter(ref HTMLNode root)
        {
            string name = null;
            string argument = null;
            int index = 0;

            for (int i = 0; i < root.TagName.Length; i++)
            {
                if (root.TagName[i] != '<')
                {
                    if (root.TagName[i] != ' ' & root.TagName[i] != '>')
                    {
                        name += root.TagName[i];
                        index++;
                    }
                    else
                        break;
                }
            }

            for (int i = index + 1; i < root.TagName.Length; i++)
            {
                if (root.TagName[i] == '>')
                {
                    if (argument != null)
                        root.Argument = argument;

                    break;
                }

                if (root.TagName[i] != ' ')
                    argument += root.TagName[i];

                else
                {
                    if (argument != null)
                    {
                        root.Argument = argument;
                        argument = null;
                    }
                }

            }

            root.TagName = name;

            for (int i = 0; i < root.ChildrenList.Count; i++)
            {
                HTMLNode nodeX = root.ChildrenList[i];
                ArgumentSplitter(ref nodeX);

                root.ChildrenList[i] = nodeX;
            }
        }
    }
}
