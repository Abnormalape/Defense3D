using BHSSolo.DungeonDefense.Data;
using System.Collections;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Dialogue

{
    public static class PrintDialogue
    {

        static private void PrintPrologueDialogue()
        {
            PrintDialogueMessage(GameData.PrologueDialogueData);
        }

        public static void PrintDialogueMessage(List<Dictionary<string, string>> dialogueDictionary)
        {   //Todo:
            // 1. Check Option.
            // 2. Find MainID.
            // 3. Select MainID.
            int mainID = 1; //Todo:
            // 4. Start Dialogue.
            List<Dictionary<string, string>> usingDialogueData = new(20);
            bool foundMainID = false;

            for (int i = 0; i < dialogueDictionary.Count; i++)
            {
                if (dialogueDictionary[i]["MainID"] != null && dialogueDictionary[i]["MainID"] != "")
                {
                    if (foundMainID)
                    {
                        break;
                    }
                    if (dialogueDictionary[i]["MainID"] == $"{mainID}")
                    {
                        foundMainID = true;
                    }
                }
                if (foundMainID)
                {
                    usingDialogueData.Add(dialogueDictionary[i]);
                }
            }

            ShowMessages(usingDialogueData);
        }

        public static IEnumerator ShowMessages(List<Dictionary<string, string>> usingDialogueData, int currentLine = 0)
        {
            List<Dictionary<string, string>> m_usingDialogueData = usingDialogueData;
            int m_currentLine = currentLine;

            UnityEngine.Debug.Log(usingDialogueData[m_currentLine]["Dialogue"]);

            if (usingDialogueData[m_currentLine]["Stream"] == "Next")
            {
                ShowMessages(m_usingDialogueData, m_currentLine + 1);
            }
            else if (usingDialogueData[m_currentLine]["Stream"] == "Choice")
            {

            }
            else if (usingDialogueData[m_currentLine]["Stream"] == "Goto")
            {

            }
            else if (usingDialogueData[m_currentLine]["Stream"] == "End")
            {
                
            }
            yield return null;
        }
    }
}
