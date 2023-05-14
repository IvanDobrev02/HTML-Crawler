using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaRabotaSAA
{
    internal class HelpFunctions
    {
        //Извежда пореден номер на всеки Node 
        public static void NumberOfNode(ref HTMLNode currNode)
        {
            int n = 1;
            string word;
            int j = currNode.ChildrenList.Count;
            for (int i = currNode.ChildrenList.Count - 1; i >= 0; i--)
            {
                word = currNode.ChildrenList[i].TagName;
                for (int c = j - 2; c >= 0; c--)
                {
                    if (word == currNode.ChildrenList[c].TagName)
                    {
                        n++;
                    }
                }
                currNode.ChildrenList[i].NumberOfNode = n;
                n = 1;
                j--;
            }

            for (int i = 0; i < currNode.ChildrenList.Count; i++)
            {
                HTMLNode nodeX = currNode.ChildrenList[i];
                NumberOfNode(ref nodeX);
                currNode.ChildrenList[i] = nodeX;
            }
        }


        //Намираме точния Node, който търсим
        public static HTMLNode FindNode(HTMLNode currNode, List<string> strings, int c)
        {
            if (c != strings.Count)
            {
                bool more = StringConverter.SquareBracketsChecker(strings[c]);
                string[] tagPlusPath = new string[1];
                if (more)
                    tagPlusPath = StringConverter.SplitByBrackets(strings[c]);

                for (int i = 0; i < currNode.ChildrenList.Count; i++)
                {
                    if (currNode.ChildrenList[i] != null)
                    {
                        if (more == false)
                        {
                            if (strings[c] == "*")
                            {
                                HTMLNode nodeX = FindNode(currNode.ChildrenList[i], strings, c + 1);
                                currNode = nodeX;
                            }
                            else if (strings[c] == currNode.ChildrenList[i].TagName)
                            {
                                HTMLNode nodeX = FindNode(currNode.ChildrenList[i], strings, c + 1);
                                currNode = nodeX;
                            }
                        }
                        else
                        {
                            int n;
                            int.TryParse(tagPlusPath[1], out n);

                            if (tagPlusPath[0] == currNode.ChildrenList[i].TagName && tagPlusPath[1] == currNode.ChildrenList[i].Argument ||
                                tagPlusPath[0] == currNode.ChildrenList[i].TagName && n == currNode.ChildrenList[i].NumberOfNode)
                            {
                                HTMLNode nodeX = FindNode(currNode.ChildrenList[i], strings, c + 1);
                                currNode = nodeX;
                            }
                        }
                    }
                }
            }
            else
            {
                bool more = StringConverter.SquareBracketsChecker(strings[c - 1]);
                string[] tagPlusPath = new string[1];
                if (more)
                    tagPlusPath = StringConverter.SplitByBrackets(strings[c - 1]);

                for (int i = 0; i < currNode.ChildrenList.Count; i++)
                {
                    if (more == false)
                        return currNode;

                    else
                    {
                        int n;
                        int.TryParse(tagPlusPath[1], out n);
                        if (tagPlusPath[0] == currNode.TagName && tagPlusPath[1] == currNode.Argument ||
                            tagPlusPath[0] == currNode.TagName && n == currNode.NumberOfNode)
                            return currNode;
                    }
                }
            }
            return currNode;
        }


        //Намираме децата на Node, който търсим
        public static void FindChildrenOfNode(HTMLNode currNode, List<string> strings, ref List<HTMLNode> returnList, int c)
        {
            if (c != strings.Count)
            {
                bool more = StringConverter.SquareBracketsChecker(strings[c]);
                string[] tagPlusPath = new string[1];
                if (more)
                    tagPlusPath = StringConverter.SplitByBrackets(strings[c]);

                for (int i = 0; i < currNode.ChildrenList.Count; i++)
                {
                    if (currNode.ChildrenList[i] != null)
                    {
                        if (more == false)
                        {
                            if (strings[c] == "*")
                            {
                                FindChildrenOfNode(currNode.ChildrenList[i], strings, ref returnList, c + 1);
                            }
                            else if (strings[c] == currNode.ChildrenList[i].TagName)
                                FindChildrenOfNode(currNode.ChildrenList[i], strings, ref returnList, c + 1);
                        }
                        else
                        {
                            int n;
                            int.TryParse(tagPlusPath[1], out n);

                            if (tagPlusPath[0] == currNode.ChildrenList[i].TagName && tagPlusPath[1] == currNode.ChildrenList[i].Argument ||
                                tagPlusPath[0] == currNode.ChildrenList[i].TagName && n == currNode.ChildrenList[i].NumberOfNode)
                                FindChildrenOfNode(currNode.ChildrenList[i], strings, ref returnList, c + 1);
                        }
                    }
                }
            }
            else
            {
                bool more = StringConverter.SquareBracketsChecker(strings[c - 1]);
                string[] tagPlusPath = new string[1];
                if (more)
                {
                    tagPlusPath = StringConverter.SplitByBrackets(strings[c - 1]);
                }

                for (int i = 0; i < currNode.ChildrenList.Count; i++)
                {
                    if (more == false)
                        returnList.Add(currNode.ChildrenList[i]);
                    else
                    {
                        int n;
                        int.TryParse(tagPlusPath[1], out n);
                        if (tagPlusPath[0] == currNode.TagName && tagPlusPath[1] == currNode.Argument ||
                            tagPlusPath[0] == currNode.TagName && n == currNode.NumberOfNode)

                            returnList.Add(currNode.ChildrenList[i]);
                    }
                }
            }
        }


        //Изграждане на текста, който ще запазим във файла
        public static void FiletextMaker(HTMLNode currNode, ref string allText)
        {
            string command = currNode.Type;
            string tagArg = currNode.Argument != null ? " " + currNode.Argument : "";

            switch (command)
            {
                case "Tag":
                    allText += StringConverter.NumberOfTabluations(currNode.NumberOfParents) + "<" + currNode.TagName + tagArg + ">" + Environment.NewLine;
                    break;

                case "SCTag":
                    allText += StringConverter.NumberOfTabluations(currNode.NumberOfParents) + "<" + currNode.TagName + tagArg + "/" + ">" + Environment.NewLine;
                    break;

                case "Value":
                    allText += StringConverter.NumberOfTabluations(currNode.NumberOfParents) + currNode.TagName + Environment.NewLine;
                    break;

            }

            for (int i = 0; i < currNode.ChildrenList.Count; i++)
            {
                FiletextMaker(currNode.ChildrenList[i], ref allText);

                if (i == currNode.ChildrenList.Count - 1)
                {
                    string text = "";
                    string command2 = currNode.Type;

                    switch (command2)
                    {
                        case "Tag":
                            text += StringConverter.NumberOfTabluations(currNode.NumberOfParents) + "</" + currNode.TagName + ">" + Environment.NewLine;
                            break;
                    }

                    allText += text;
                }
            }
        }

    }
}
