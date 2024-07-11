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

        //용사를 생성할 때, 대략적인 수치는 용사의 타입에 맞게 설정이 된다.
        //상세수치는 정해진 수치 내에서 변동되어 생성된다.
        //용사는 실시간으로 레벨업을 할 수 있다. 만, 잘 일어나지 않는다.
        //같은 종류의 용사여도 더 높은 레벨을 지녔다면, 능력치가 더 높다.
        //용사는 타입에 따라 기본적으로 몇가지 특성을 가지고 시작한다. (ex.왕궁기사 : 제식검법, 왕궁검법)
        //용사는 타입에 따른 특성 외에도 npc특성을 가지고 시작할 수 있다.
        //용사는 장비를 착용 할 수 있다.

        private void InitNPCDataTable()
        {
            //NPCData = ;//Todo:여기서 입력받은 HeroType으로 NPCData를 저장.
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
