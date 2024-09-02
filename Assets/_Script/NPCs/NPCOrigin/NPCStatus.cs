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
    class NPCStatus
    {
        private int healthPoint;
        private int manaPoint;
        private int strength;
        private int intelligence;
        private int toughness;
        private int mindness;
        private int agility;

        public NPCStatus(
            int inputHP,
            int inputMP,
            int inputStr,
            int inputInt,
            int inputThn,
            int inputMdn,
            int inputAgl)
        {
            healthPoint = inputHP;
            manaPoint = inputMP;
            strength = inputStr;
            intelligence = inputInt;
            toughness = inputThn;
            mindness = inputMdn;
            agility = inputAgl;
        }
    }
}
