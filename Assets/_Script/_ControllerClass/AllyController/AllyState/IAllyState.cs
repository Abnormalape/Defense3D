namespace BHSSolo.DungeonDefense.Controller
{
    public interface IAllyState
    {
        public AllyController_ AllyController { get; set; }
        public AllyStates AllyState { get; set; }
        public void InitializeAllyState(AllyController_ allyController);
    }

    public enum AllyStates
    {
        Idle, //대기중 -> 방에서 기다리고 있는 상태
        Moving, //이동중 -> 목적지를 설정하고 이동, 이동 중 조우시 전투상태로
        Battle, //전투중 -> 전투
    }
}