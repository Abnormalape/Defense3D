using BHSSolo.DungeonDefense.StaticFunction;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace BHSSolo.DungeonDefense.NPCs
{

    class Hero : NPCs
    {

        public HeroType HeroType { get; private set; }
        public Hero(HeroType heroType)
        {   //level:1, equipment null 의 hero.
            InitNPCDataTable(heroType);
            InitHeroStatus(heroType);
            InitHeroState();
        }
        public Hero(HeroType heroType, int level)
        {   //level이 바뀌었고, equipment null의 hero.
            NPCLevel = level;
            InitNPCDataTable(heroType);
            InitHeroStatus(heroType);
            InitHeroState();
        }

        public Hero(HeroType heroType, List<NPCEquipment> equipments)
        {   //level:1, equipment !null 의 hero.
            InitNPCDataTable(heroType);
            InitHeroStatus(heroType);
            InitHeroState();
            InitHeroEquipment(equipments);
            InitHeroStateWithEquipment();
        }

        public Hero(HeroType heroType, int level, List<NPCEquipment> equipments)
        {   //level이 바뀌었고, equipment !null 의 hero.
            NPCLevel = level;
            InitNPCDataTable(heroType);
            InitHeroStatus(heroType);
            InitHeroState();
            InitHeroEquipment(equipments);
            InitHeroStateWithEquipment();
        }

        private void InitNPCDataTable(HeroType heroType)
        {
            NPCData = NPCDataLoader.FindHeroFromHeroDictionary(heroType.ToString());
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

        //Hero의 Equipment초기화.
        private void InitHeroEquipment(List<NPCEquipment> equipment)
        {
            nPCEquipment = equipment; //장비창에 입력받은 리스트를 넣는다.
        }

        //Hero의 State초기화.
        //nature와 equipment가 제공하는 State를 저장한다.
        private void InitHeroState()
        {
            //Todo: Type이 가진 특성에서 파생하는 효과를 지닌다.
            int stateCount = Convert.ToInt32(NPCData["StateCount"]);

            for (int ix = 0; ix < stateCount; ++ix)
            {
                //Todo: 현재 Room의 State 부여 방식은 잘못 되어있다.
                //1. HeroData에서 StateCount를 찾아서 그 수만큼 순회한다.
                //2. State{ix}는 상태의 이름이다.
                string stateName = NPCData[$"State{ix + 1}"];
                //3. 위의 이름을 NPCStateData에서 찾아낸다. Todo: 현재 NPCStateData가 없다.

                //4. NPCState에 new로 생성하면서 등록한다.

            }

            //Todo: 동시에 누가 주는 효과인지 명시해야한다.
        }

        private void InitHeroStateWithEquipment()
        {
            //Todo: equipment가 부여한 특성에서 파생하는 효과를 지닌다.

            //Todo: 동시에 누가 주는 효과인지 명시해야한다.
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
