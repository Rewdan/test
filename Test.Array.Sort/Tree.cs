using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Test.CustomArray.Sort
{

    public class Tree
    {
        public Tree(string fileName,Tree parent =null)
        {
            FileName = fileName;
            Parent = parent;
        }

        public void SetChild(Tree[]  tree)
        {
            Children= (tree);
        }
        public string FileName { get; set; }

        public Tree[] Children { get; private set; } = new Tree[2];

        public Tree Parent { get; private set; } 
    }

}
