using BHSSolo.DungeonDefense.StaticFunction;
using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    class Monster : NPC
    {
        public MonsterType MonsterType { get; private set; }
        public Monster(MonsterType monsterType)
        {
            InitNPCID();
            InitMonsterDictionary(monsterType);
            InitMonsterStatus(monsterType);
            InitMonsterTrait();
            InitMonsterBuff();
        }

        public Monster(MonsterType monsterType, int level)
        {
            InitNPCID();
            NPCLevel = level;
            InitMonsterDictionary(monsterType);
            InitMonsterStatus(monsterType);
            InitMonsterTrait();
            InitMonsterBuff();
        }

        public Monster(MonsterType monsterType, List<NPCEquipment> equipments)
        {
            InitNPCID();
            InitMonsterDictionary(monsterType);
            InitMonsterStatus(monsterType);
            InitMonsterTrait();
            InitMonsterBuff();
            //InitMonsterEquipment(equipments);
            //InitMonsterBuffWithEquipment();
        }

        public Monster(MonsterType monsterType, int level, List<NPCEquipment> equipments)
        {
            InitNPCID();
            NPCLevel = level;
            InitMonsterDictionary(monsterType);
            InitMonsterStatus(monsterType);
            InitMonsterTrait();
            InitMonsterBuff();
            //InitMonsterEquipment(equipments);
            //InitMonsterBuffWithEquipment();
        }

        private void InitMonsterDictionary(MonsterType monsterType)
        {
            NPCData = FindSingleDictionaryFromDictionaryList.FindDictionaryByKey
                (NPCDatas.NPCMonsterData, "MonsterType", monsterType.ToString());
        }

        private void InitMonsterStatus(MonsterType monsterType)
        {
            NPCType = NPCType.Monster; //npc 타입은 Monster 이고
            MonsterType = monsterType;//Monster 타입은 입력받은 Monster 타입.
            NPCName = ""; // Todo: 랜덤한 이름.

            //저장된 NPCData를 바탕으로, 새로운 스탯을 생성한다.
            nPCStatus = new NPCStatus(
                Convert.ToInt32(NPCData["HealthPoint"]),
                Convert.ToInt32(NPCData["ManaPoint"]),
                Convert.ToInt32(NPCData["Strength"]),
                Convert.ToInt32(NPCData["Intelligence"]),
                Convert.ToInt32(NPCData["Toughness"]),
                Convert.ToInt32(NPCData["Mindness"]),
                Convert.ToInt32(NPCData["Agility"]));
            //Todo: random하게 스탯에 변화를 준다.
            //Todo: level에 맞게 스탯에 변화를 준다.
        }

        private void InitMonsterTrait()
        {
            int traitCount = Convert.ToInt32(NPCData["TraitCount"]);

            for (int ix = 0; ix < traitCount; ++ix)
            {   //nPCTrait에 new Trait()로 Add.
                nPCTrait.Add(new NPCTrait(NPCData[$"Trait{ix + 1}"]));
            }
        }

        private void InitMonsterBuff()
        {
            int traitCount = nPCTrait.Count;
            for (int ix = 0; ix < traitCount; ++ix)
            {
                int buffCount = Convert.ToInt32(nPCTrait[ix].traitData["BuffCount"]);
                for (int iy = 0; iy < buffCount; ++iy)
                {
                    //nPCBuff.Add(new NPCBuff(nPCTrait[ix].traitData[$"Buff{iy}"])); //Todo:
                }
            }
        }
    }

    public enum MonsterType
    {
        Slime,
        Goblin,
        Orc,
    }
}
