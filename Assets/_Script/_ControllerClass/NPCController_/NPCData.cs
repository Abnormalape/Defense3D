namespace BHSSolo.DungeonDefense.Controller
{
    public class NPCData
    {
        public NPCData(NPCStatus inputStatus)
        {
            NPCStatus = new(inputStatus);
        }


        public readonly NPCStatus NPCStatus;
    }
}
