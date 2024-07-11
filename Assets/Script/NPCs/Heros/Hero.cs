using System.Net.NetworkInformation;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// HealthPoint : 체력
    /// ManaPoint : 마력
    /// Strength : 근력 (물리적 데미지 계산, 공격속도)
    /// Intelligence : 지력 (마법적 데미지 계산, 공격속도)
    /// Toughness : 내구 (데미지 경감)
    /// Mindness : 정신력 (상태이상등 해로운 효과 저항)
    /// Agility : 반사신경 (행동속도 보정)
    /// 상기 사항들은 성장 보정치를 의미함.
    /// </summary>
    class Hero : NPCs
    {
        private int healthPoint; //Todo: 얘들을 NPCStatus라는 별도의 클래스에 놓는게 맞을듯하다.
        private int manaPoint;
        private int strength;
        private int intelligence;
        private int toughness;
        private int mindness;
        private int agility;
        public HeroType HeroType { get; private set; }
        public Hero(HeroType heroType)
        {   //level:1, equipment null 의 hero.
            
        }
        public Hero(HeroType heroType, int level)
        {   //level이 바뀌었고, equipment null의 hero.
            NPCLevel = level;
            
        }

        public Hero(HeroType heroType, NPCEquipment[] equipments)
        {   //level:1, equipment !null 의 hero.

        }

        public Hero(HeroType heroType, int level, NPCEquipment[] equipments)
        {   //level이 바뀌었고, equipment !null 의 hero.
            NPCLevel = level;
            
        }

        private void InitNPCDataTable(HeroType heroType)
        {
            
        }

        //Hero의 status초기화.
        private void InitHeroStatus(HeroType heroType)
        {
            NPCType = NPCType.Hero; //npc 타입은 히어로 이고
            HeroType = heroType;    //hero 타입은 입력받은 히어로 타입.
            NPCName = ""; // 랜덤한 이름.

            //입력받은 히어로 타입을 히어로 타입csv를 분해한 herotypedata에서 찾아서 저장한다. Todo:
            //NPCData.; //Todo:

            //저장 할때 level을 이용하여 스탯에 변화를준다.

            //저장 할때 random하게 스탯에 변화를 준다.

        }

        //Hero의 Equipment초기화.
        private void InitHeroEquipment(NPCEquipment[] equipment)
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
