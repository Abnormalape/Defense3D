using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    public class NPCTraitController
    {
        private readonly NPC _ownerNPC;

        public Dictionary<string, NPCTrait> NPCTraits { get; private set; } = new(3);

        public NPCTraitController(NPC owner)
        {
            _ownerNPC = owner;

            InitiateTrait();
        }

        private void InitiateTrait()
        {
            // _ownerNPC. //Todo: Get data from _ownerNPC
            int m_traitCount = Convert.ToInt32(_ownerNPC.NPCData["TraitCount"]);

            for (int ix = 1; ix <= m_traitCount; ix++)
            {
                string m_traitName = _ownerNPC.NPCData[$"Trait{ix}"];

                Dictionary<string, string> m_traitData //Todo:
                    = Data.GameData.NPCTraitData[m_traitName];

                NPCTrait m_trait = new NPCTrait(this, m_traitData);

                AddTrait(m_traitName, m_trait);
            }
        }

        private void AddTrait(string traitName,NPCTrait traitToAdd)
        {
            NPCTraits.Add(traitName,traitToAdd); //Todo: check exception.
            //Todo: If their is trait already exist, 1. don't add /or/ 2. evolve.
        }

        private void RemoveTrait(string traitName)
        {
            NPCTraits.Remove(traitName);
        }
    }
}
