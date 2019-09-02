using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public partial class Tree
    {
        public string title;

        public List<Tree> children = new List<Tree>();
    }
}