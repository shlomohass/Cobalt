using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser
{
    public abstract class SyntaxNode
    {
        private List<SyntaxNode> leaves = new List<SyntaxNode>();

        public IList<SyntaxNode> Leaves
        {
            get
            {
                return this.leaves;
            }
        }

        public void AddLeave(SyntaxNode node)
        {
            this.leaves.Add(node);
        }
    }
}
