using System.Collections.Generic;
using Unity.VisualScripting;

namespace BHSSolo.DungeonDefense.UI
{
    public class UI_AccepCancle
    {
        //메세지, 버튼 버튼


        List<int> dd = new();
        public void asdf()
        {
            List<int> dfa = new();

            for (int i = 0; dd.Count < 0; i++)
            {
                if (dd[i] >= 3)
                {
                    dfa.Add(dd[i]);
                }
            }
            //둘은 같은 역할을 한다. 그런데 List에는 Find가 없는데, 어떻게 된 일 일까? 그것은 바로 확장함수.
            //static인 확장함수 때문이다.
            //reflection.
            dfa.Find(x => x >= 3);
        }
    }
}
