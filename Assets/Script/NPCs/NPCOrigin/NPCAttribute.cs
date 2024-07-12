using System.Collections.Generic;
using BHSSolo.DungeonDefense.StaticFunction;

namespace BHSSolo.DungeonDefense.NPCs
{

    class NPCAttribute
    {
        public Dictionary<string, string> attributeData { get; private set; }

        public object attributeGiver { get; private set; } //특성제공자.

        public NPCAttribute(string attributeName)
        {
            attributeData = FindSingleDictionaryFromDictionaryList.FindDictionaryByKey(
                NPCDatas.NPCAttributeData, "AttributeName", attributeName);
        }
    }

    public enum NPCAttributeName
    {
        SharpClaw,
    }
}
