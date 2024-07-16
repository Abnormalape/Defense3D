﻿using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.StaticFunction
{
    /// <summary>
    /// 입력한 조건에 맞는 Dictionary를 찾아주는 클래스 이다.
    /// </summary>
    static class FindSingleDictionaryFromDictionaryList
    {
        //Test Dictionary for better Data Saving.
        static Dictionary<int,Dictionary<string,string>> tempDictDict;

        /// <summary>
        /// this method Find out single dictionary from List<dictionary<>> with key and string.
        /// Todo: But if We Use Dictionary<int,Dictionary<>> it can be better
        /// </summary>
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

        /// <summary>
        /// Dictionary Dictionary.
        /// Todo: Need to Convert List<Dictionary> datas to Dictionary<string, Dictionary<>> data.
        /// </summary>
        public static Dictionary<string, string> FindDictionaryByDictionary
            (Dictionary<string,Dictionary<string,string>> list, string DicionaryName)
        {
            Dictionary<string, string> outDictionary = new Dictionary<string, string>();

            outDictionary = list[DicionaryName];

            return outDictionary;
        }
    }
}
