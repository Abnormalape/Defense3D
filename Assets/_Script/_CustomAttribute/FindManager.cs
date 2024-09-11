using System;

namespace BHSSolo.DungeonDefense.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FindManager : Attribute
    {
        public FindManager()
        {

        }
    }
}
