using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.StaticFunction
{
    /// <summary>
    /// 입력한 조건에 맞는 Dictionary를 찾아주는 클래스 이다.
    /// </summary>
    static class FindSingleDictionaryFromDictionaryList
    {
        public static Dictionary<string, string> FindDictionaryByKey(List<Dictionary<string,string>> list,string key,string value)
        {
            Dictionary<string, string> outDictionary = new Dictionary <string, string>();
            List<Dictionary<string, string>> tempDictionary = list;

            for (int ix = 0; ix < tempDictionary.Count; ++ix)
            {
                if (tempDictionary[ix][key] == value)
                { outDictionary = tempDictionary[ix]; break; }
            }
            return outDictionary;
        }
    }
}
