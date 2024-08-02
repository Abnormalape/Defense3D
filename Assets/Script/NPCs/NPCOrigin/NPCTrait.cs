using System.Collections.Generic;
using BHSSolo.DungeonDefense.Function;

namespace BHSSolo.DungeonDefense.NPCs
{

    class NPCTrait
    {
        public Dictionary<string, string> traitData { get; private set; }

        public object traitGiver { get; private set; } //특성제공자.

        //Todo: Trait Initializer
        public NPCTrait(string traitName)
        {
            traitData = FindSingleDictionaryFromMultipleDictionary.FindDictionaryFromDictionary
                (Data.GameData.NPCTraitData, traitName);
        }
    }

    public enum NPCTraitName
    {
        SharpClaw,
    }
}
