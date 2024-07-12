using System.Collections.Generic;
using BHSSolo.DungeonDefense.StaticFunction;

namespace BHSSolo.DungeonDefense.NPCs
{

    class NPCTrait
    {
        public Dictionary<string, string> traitData { get; private set; }

        public object traitGiver { get; private set; } //특성제공자.

        public NPCTrait(string traitName)
        {
            traitData = FindSingleDictionaryFromDictionaryList.FindDictionaryByKey(
                NPCDatas.NPCTraitData, "TraitName", traitName);
        }
    }

    public enum NPCTraitName
    {
        SharpClaw,
    }
}
