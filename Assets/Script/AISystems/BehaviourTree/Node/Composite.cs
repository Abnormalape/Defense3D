using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.AISystem
{
    public abstract class Composite : Node, IParentOfChildren
    {
        protected Composite(BehaviourTree tree) : base(tree)
        {
            children = new List<Node>();
        }

        public List<Node> children { get; set; }
        protected int currentChildIndex; //To Run Next Node.
    }
}
