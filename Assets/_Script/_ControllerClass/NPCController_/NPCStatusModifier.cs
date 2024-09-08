namespace BHSSolo.DungeonDefense.Controller
{
    public class NPCStatusModifier
    {
        /// <summary>
        /// NPC Like Ally and Enemy, NamedNPC(Enemy, Ally) should have this
        /// </summary>
        public NPCStatusModifier()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public void MaxStatusModifier()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void CurrentStatusModifier()
        {

        }

        //스테이터스의 종류.
        //1. 기본값 : 말 그대로 아무 것도 없는 상태의 수치이다.
        //2. 중간값 : 특성 및 장비로 스탯이 추가된 상태의 수치이다.
        //3. 최종값 : 버프 디버프 등을 통하여 증폭, 감쇠 되는 수치이다.

        // 즉 그냥 NPC하나가 있다면 기본값이.
        // 특성과 장비에 의한 조정은 기본값을 바탕으로
        // 버프 디버프등에 의한 보정은 기본값을 바탕으로 하거나, 중간값을 바탕으로 한다.

        //누군가와 상호작용 할 때는 기본적으로 최종값을 바탕으로 하나, 때로는 중간값을 바탕으로 하기도, 기본값을 바탕으로 하기도 한다.

        //외부에서 NPC의 스탯을 조정 하고 싶다면, NPCController.NPCStatusModifier 를 통한다.
    }
}
