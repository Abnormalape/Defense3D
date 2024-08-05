using BHSSolo.DungeonDefense.Management;
using BHSSolo.DungeonDefense.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BHSSolo.DungeonDefense.Scene
{
    public class TitleSceneController : SingletonMono<TitleSceneController>
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _startButton;
        [SerializeField] private GameObject _loadButton;
        [SerializeField] private GameObject _optionButton;


        private void Awake()
        {
            if (_gameManager != null) { FindFirstObjectByType(typeof(GameManager)); }
        }

        private void Start()
        {
            if (_gameManager.IsSaveGameDataExsist)
                _loadButton.GetComponent<Button>().interactable = true;
            else
                _loadButton.GetComponent<Button>().interactable = false;
        }
    }
}
