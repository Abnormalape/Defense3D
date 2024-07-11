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

        }
        public Hero(HeroType heroType, int level)
        {   //level이 바뀌었고, equipment null의 hero.
            NPCLevel = level;
            InitNPCDataTable(heroType);

        }

        public Hero(HeroType heroType, NPCEquipment[] equipments)
        {   //level:1, equipment !null 의 hero.
            InitNPCDataTable(heroType);

        }

        public Hero(HeroType heroType, int level, NPCEquipment[] equipments)
        {   //level이 바뀌었고, equipment !null 의 hero.
            NPCLevel = level;
            InitNPCDataTable(heroType);

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
            //Todo: 저장 할때 random하게 스탯에 변화를 준다.

        }

        //Hero의 Equipment초기화.
        private void InitHeroEquipment(List<NPCEquipment> equipment)
        {   
            nPCEquipment = equipment;
        }

        //Hero의 State초기화.
        //nature와 equipment가 제공하는 State를 저장한다.
        private void InitHeroState()
        {

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
