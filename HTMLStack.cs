using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaRabotaSAA
{
    internal class HTMLStack
    {
        static readonly int MAX = 1000;
        int top;
        string[] stack = new string[MAX];

        public bool IsEmpty()
        {
            return (top < 0);
        }
        public HTMLStack()
        {
            top = -1;
        }
        internal void Push(string data)
        {
            if (top >= MAX)
                throw new ArgumentOutOfRangeException("Parameter index is out of range.");
            else
                stack[++top] = data;
        }

        internal string Pop()
        {
            string value = stack[top--];
            return value;
        }

        internal string Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return null;
            }
            else
                return stack[top];
        }
    }
}
