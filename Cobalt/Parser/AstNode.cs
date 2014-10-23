using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser
{
    public abstract class AstNode
    {
        private List<AstNode> leaves = new List<AstNode>();

        public IList<AstNode> Leaves
        {
            get
            {
                return this.leaves;
            }
        }

        public void AddLeave(AstNode node)
        {
            this.leaves.Add(node);
        }
    }
}
