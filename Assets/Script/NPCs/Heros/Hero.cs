using BHSSolo.DungeonDefense.StaticFunction;
using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{

    class Hero : NPC
    {
        public HeroType HeroType { get; private set; }
        public Hero(HeroType heroType)
        {   //level:1, equipment null 의 hero.
            InitHeroDataDictionary(heroType);
            InitHeroStatus(heroType);
            InitHeroTrait();
            InitHeroBuff();
        }
        public Hero(HeroType heroType, int level)
        {   //level이 바뀌었고, equipment null의 hero.
            NPCLevel = level;
            InitHeroDataDictionary(heroType);
            InitHeroStatus(heroType);
            InitHeroTrait();
            InitHeroBuff();
        }

        public Hero(HeroType heroType, List<NPCEquipment> equipments)
        {   //level:1, equipment !null 의 hero.
            InitHeroDataDictionary(heroType);
            InitHeroStatus(heroType);
            InitHeroTrait();
            InitHeroBuff();
            //InitHeroEquipment(equipments);
            //InitHeroBuffWithEquipment();
        }

        public Hero(HeroType heroType, int level, List<NPCEquipment> equipments)
        {   //level이 바뀌었고, equipment !null 의 hero.
            NPCLevel = level;
            InitHeroDataDictionary(heroType);
            InitHeroStatus(heroType);
            InitHeroTrait();
            InitHeroBuff();
            //InitHeroEquipment(equipments);
            //InitHeroBuffWithEquipment();
        }

        private void InitHeroDataDictionary(HeroType heroType)
        {
            NPCData = FindSingleDictionaryFromDictionaryList.FindDictionaryByKey
                (NPCDatas.NPCHeroData,"HeroType",heroType.ToString());
        }

        //Hero의 status초기화.
        private void InitHeroStatus(HeroType heroType)
        {
            NPCType = NPCType.Hero; //npc 타입은 히어로 이고
            HeroType = heroType;    //hero 타입은 입력받은 히어로 타입.
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

        //Hero의 Trait를 NPCData의 [Trait]기준으로 초기화.
        private void InitHeroTrait()
        {
            int traitCount = Convert.ToInt32(NPCData["TraitCount"]);

            for (int ix = 0; ix < traitCount; ++ix)
            {   //nPCTrait에 new Trait()로 Add.
                nPCTrait.Add(new NPCTrait(NPCData[$"Trait{ix + 1}"]));
            }
        }

        //Todo: 현재 Room의 State 부여 방식은 잘못 되어있다.
        //Hero의 Buff를 nPCTrait의 [Buff]기준으로 초기화.
        private void InitHeroBuff()
        {   //Trait가 Buff를 제공함.
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

        private void InitHeroStateWithEquipment()
        {
            //Todo: equipment가 부여한 특성에서 파생하는 효과를 지닌다.

            //Todo: 동시에 누가 주는 효과인지 명시해야한다.
        }

        //Hero의 Equipment초기화.
        private void InitHeroEquipment(List<NPCEquipment> equipment)
        {
            nPCEquipment = equipment; //장비창에 입력받은 리스트를 넣는다.
        }

        public void GudokRoomEffect() //Todo: 임시코드.
        {

        }
    }

    public enum HeroType //HeroType으로 Status배분과 nature가 정해진다.
    {
        Archer,
        Swordman,
        Explorer,
        Saint,
    }
}
