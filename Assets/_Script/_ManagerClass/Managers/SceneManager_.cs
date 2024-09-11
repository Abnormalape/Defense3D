using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class SceneManager_ : MonoBehaviour, IManagerClass
    {
        public delegate void NewSceneActivated();
        public event NewSceneActivated OnNewSceneActivated; //Add Some Method To This Event.


        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; }
        public GameManager_ OwnerManager { get; set; }
        public float LoadingProgress { get; private set; }


        private UIManager_ UIManager_;



        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            UIManager_ = OwnerManager.UIManager_;
        }

        public void OrderChangeScene(string sceneName)
        {
            StartCoroutine(ChangeScene(sceneName));
        }

        public IEnumerator ChangeScene(string sceneName)
        {
            UIManager_.Open(UI_enum.LoadScene);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
            {
                LoadingProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                yield return null;
            }
            LoadingProgress = 1.0f;

            yield return new WaitForSeconds(2f);


            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone)
            {
                yield return null;
            }            

            OnNewSceneActivated?.Invoke();

            UIManager_.Close(UI_enum.LoadScene);
        }
    }
}
