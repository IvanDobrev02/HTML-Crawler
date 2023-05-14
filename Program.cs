using System;
using System.Collections.Generic;
using System.IO;

namespace KursovaRabotaSAA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("insert file path: ");
            string path = Console.ReadLine();

            string textFromFile = File.ReadAllText(path);

            CodeChecker.Test(textFromFile);
            List<string> raw = new List<string>(TreeMaker.ElementSplitter(textFromFile));

            HTMLNode firstNode = new HTMLNode("<html>", "Tag", 0, null, null, new List<HTMLNode>(), 1);
            TreeMaker.TreeBuilding(raw, ref firstNode);
            TreeMaker.ArgumentSplitter(ref firstNode);
            HelpFunctions.NumberOfNode(ref firstNode);
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - View the tree \n2 - Search \n3 - Change the children-nodes \n4 - Copy the content of node \n5 - Save the new HTML code \n6 - Exit");
                var key = Console.ReadKey().KeyChar;

                Console.Clear();

                switch (key)
                {
                    case '1':
                        {
                            MainFunctions.TreeVisualization(firstNode, 0);
                            Console.ReadLine();
                        }
                        break;
                    case '2':
                        {
                            Console.WriteLine("Insert tree-path for searching : ");
                            string searchText = Console.ReadLine();
                            Console.WriteLine();

                            if (searchText == "//")
                            {
                                MainFunctions.TreeVisualization(firstNode, 0);
                            }
                            else
                            {
                                List<string> comandList = StringConverter.SplitBySlash(searchText);
                                MainFunctions.HuntNode(firstNode, comandList, 1);
                            }
                            Console.ReadLine();
                        }
                        break;
                    case '3':
                        {
                            Console.Write("Insert the tree-path to the wanted node and the new text after tabulation: ");
                            string txt = Console.ReadLine();

                            string[] arr = StringConverter.SplitBySpace(txt);
                            List<string> comandList = StringConverter.SplitBySlash(arr[0]);
                            MainFunctions.SetNewNameToNode(ref firstNode, comandList, arr[1], 1);
                            HelpFunctions.NumberOfNode(ref firstNode);

                            Console.WriteLine();
                        }
                        break;
                    case '4':
                        {
                            Console.WriteLine("Insert the tree-path to the first node and the tree-path the the second node, in which gonna be pasted the information of the first : ");
                            string txt = Console.ReadLine();

                            string[] arr = StringConverter.SplitBySpace(txt);
                            List<string> fisrtNodePath = StringConverter.SplitBySlash(arr[0]);
                            List<string> secondNodePath = StringConverter.SplitBySlash(arr[1]);

                            MainFunctions.CopyFromNode(ref firstNode, fisrtNodePath, secondNodePath, 1);
                            HelpFunctions.NumberOfNode(ref firstNode);

                            Console.ReadLine();
                        }
                        break;
                    case '5':
                        {
                            try
                            {
                                string fileText = "";
                                string filePathSaving = @"C:\Users\IvanD\Desktop\newTextFile.txt";
                                HelpFunctions.FiletextMaker(firstNode, ref fileText);
                                MainFunctions.SaveTheNewFile(filePathSaving, fileText);
                            }
                            catch
                            {
                                Console.WriteLine("Error");
                                return;
                            }

                            Console.WriteLine("Saved");
                            Console.ReadLine();

                        }
                        break;
                    case '6':
                        return;
                }
            }
        }
    }
}
