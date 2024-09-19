using System;

namespace BHSSolo.DungeonDefense.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class FindManager : Attribute
    {
        public Object targetManager { get; }
        public FindManager(object targetManager)
        {
            this.targetManager = targetManager;
        }
    }
}
