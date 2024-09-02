using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.AISystem
{
    public interface IParentOfChildren
    {
        List<Node> children { get; set; }
    }
}