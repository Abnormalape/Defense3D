using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// List<Dictionary<>> 에서 조건에 맞는 Dictionary를 반환하는 클래스이다.
    /// </summary>
    static class NPCDataLoader
    {
        public static Dictionary<string, string> FindHeroFromHeroDictionary(string heroType)
        {
            Dictionary<string, string> outHeroDictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> tempHeroDictionary =
                GetNPCTypeDataFromCSV.NPCTypeData;

            for (int ix = 0; ix < tempHeroDictionary.Count; ++ix)
            {
                if (tempHeroDictionary[ix]["HeroType"].Equals(heroType))
                { outHeroDictionary = tempHeroDictionary[ix]; break; }
            }
            return outHeroDictionary;
        }
        //Hero뿐 아니라 Monster의 데이터도 찾을 수 있어야 한다.
    }
}
