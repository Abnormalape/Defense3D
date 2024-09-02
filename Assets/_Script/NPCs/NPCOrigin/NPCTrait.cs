using System;
using System.Collections.Generic;
using BHSSolo.DungeonDefense.Function;
using Unity.VisualScripting;

namespace BHSSolo.DungeonDefense.NPCs
{

    public class NPCTrait
    {
        public Dictionary<string, string> traitData { get; private set; }

        public object traitGiver { get; private set; } //특성제공자.

        private NPCTraitController _traitHolder;

        public List<string> BuffNames { get; private set; } = new(3);

        //Todo: Trait Initializer
        public NPCTrait(NPCTraitController traitHolder, Dictionary<string, string> m_traitData)
        {
            traitData = m_traitData;
            _traitHolder = traitHolder;
            InitiateaTraitBuffNames();

            //traitData = FindSingleDictionaryFromMultipleDictionary.FindDictionaryFromDictionary
            //    (Data.GameData.NPCTraitData, m_traitName);
        }

        private void InitiateaTraitBuffNames()
        {
            int m_traitBuffCounts = Convert.ToInt32(traitData["BuffCounts"]);

            for (int ix = 1; ix <= m_traitBuffCounts; ix++)
            {
                BuffNames.Add(traitData[$"Buff{ix}"]);
            }
        }
    }

    public enum NPCTraitName
    {
        SharpClaw,
    }
}
