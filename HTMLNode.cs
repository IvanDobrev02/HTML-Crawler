using System.Collections.Generic;

namespace KursovaRabotaSAA
{
    internal class HTMLNode
    {
        public string Type { get; set; }
        public string TagName { get; set; }
        public int NumberOfParents { get; set; }
        public string Argument { get; set; }
        public HTMLNode Parent { get; set; }
        public List<HTMLNode> ChildrenList { get; set; }
        public int NumberOfNode { get; set; }
        public HTMLNode(string name, string nodeType, int parentExist, string tagArguments, HTMLNode parent, List<HTMLNode> children, int numberInCount)
        {
            Type = nodeType;
            TagName = name;
            NumberOfParents = parentExist;
            Argument = tagArguments;
            Parent = parent;
            ChildrenList = children;
            NumberOfNode = numberInCount;
        }
    }
}
