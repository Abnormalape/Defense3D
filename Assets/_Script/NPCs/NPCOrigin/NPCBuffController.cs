using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    public class NPCBuffController
    {
        private readonly NPC _ownerNPC;

        public Dictionary<string, NPCBuff> _NPCBuffs { get; private set; } = new(10);


        public NPCBuffController(NPC owner)
        {
            _ownerNPC = owner;

            InitiateTraitBuff();
        }

        private void InitiateTraitBuff()
        {
            int m_traitCounts = _ownerNPC.NPCTraitController.NPCTraits.Count;
            Dictionary<string, NPCTrait> m_traits = _ownerNPC.NPCTraitController.NPCTraits;

            foreach (var trait in m_traits)
            {
                int m_traitBuffCounts = trait.Value.BuffNames.Count;

                List<string> m_traitBuffNames = trait.Value.BuffNames;

                for (int ix = 0; ix < m_traitBuffCounts; ix++)
                {
                    string m_traitBuffName = m_traitBuffNames[ix];

                    Dictionary<string, string> m_traitBuffData //Todo:
                     = Data.GameData.BuffData[m_traitBuffName];

                    NPCBuff m_traitBuff = new NPCBuff(this, m_traitBuffData);

                    AddBuff(m_traitBuffName, m_traitBuff);
                }
            }
        }

        public void AddBuff(string buffName, NPCBuff AddedBuff)
        {
            _NPCBuffs.Add(buffName, AddedBuff);
        }

        public void RemoveBuff(string buffName)
        {
            _NPCBuffs.Remove(buffName);
        }
    }
}
