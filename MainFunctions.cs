using System;
using System.Collections.Generic;
using System.IO;

namespace KursovaRabotaSAA
{
    internal class MainFunctions
    {

        //Визуализация на дървото
        public static void TreeVisualization(HTMLNode root, int depth)
        {
            if (root != null)
            {
                Console.WriteLine(StringConverter.NumberOfTabluations(depth) + root.TagName);

                for (int i = 0; i < root.ChildrenList.Count; i++)
                {
                    TreeVisualization(root.ChildrenList[i], depth + 1);
                }
            }
        }


        //Търсене на Node, като на конзолата се изписват неговите деца
        public static void HuntNode(HTMLNode currNode, List<string> strings, int c)
        {
            List<HTMLNode> myList = new List<HTMLNode>();
            HelpFunctions.FindChildrenOfNode(currNode, strings, ref myList, c);

            for (int i = 0; i < myList.Count; i++)
            {
                Console.Write($"'{myList[i].TagName}' ");
            }
        }


        //Променяме децата на даден Node
        public static void SetNewNameToNode(ref HTMLNode currNode, List<string> strings, string newText, int c)
        {
            List<HTMLNode> myList = new List<HTMLNode>();
            HelpFunctions.FindChildrenOfNode(currNode, strings, ref myList, c);

            for (int i = 0; i < myList.Count; i++)
            {
                myList[i].TagName = newText;
            }
        }


        //Копира информацията от един Node и я прехвърля в друг
        public static void CopyFromNode(ref HTMLNode currNode, List<string> firstNodePath, List<string> secondNodePath, int c)
        {
            HTMLNode nodeX = HelpFunctions.FindNode(currNode, firstNodePath, c);
            HTMLNode nodeY = HelpFunctions.FindNode(currNode, secondNodePath, c);

            if (nodeX != null)
            {
                nodeY.ChildrenList = nodeX.ChildrenList;
                nodeY.TagName = nodeX.TagName;
                nodeY.Argument = nodeX.Argument;
                nodeY.Type = nodeX.Type;
            }
        }


        //Запазване на тексът във файл
        public static void SaveTheNewFile(string filePath, string fileText)
        {
            File.WriteAllText(filePath, fileText);
        }

    }
}
