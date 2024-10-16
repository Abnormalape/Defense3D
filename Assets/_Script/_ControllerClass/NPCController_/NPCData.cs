namespace BHSSolo.DungeonDefense.Controller
{
    public class NPCData
    {
        public NPCData(NpcBaseStatus inputStatus)
        {
            NPCStatus = new(inputStatus);
        }


        public readonly NpcBaseStatus NPCStatus;
    }
}
