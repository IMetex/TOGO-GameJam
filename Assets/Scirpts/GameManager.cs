using System;
using Scirpts.Enemy;
using Scirpts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scirpts
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject _playScreen;
        public GameObject _mainMenu;
        public GameObject _loseScren;
        public GameObject _NEXTScreen;
        public AudioSource  _MUCİS;

        private void Start()
        {
            _playScreen.SetActive(true);
            _mainMenu.SetActive(false);
            _loseScren.SetActive(false);
        }

        public void PlayButton()
        {
            _playScreen.SetActive(false);
            _mainMenu.SetActive(true);
            _MUCİS.Play();
        }
        
        public void RestarBtn()
        {
            SceneManager.LoadScene(0);
        }

        public void YouLose()
        {
            _mainMenu.SetActive(false);
            _loseScren.SetActive(true);
        }

        public void NewLevel()
        {
            if (UnitsManager.Instance.enemies.Count <= 0)
            {
                _mainMenu.SetActive(false);
                _loseScren.SetActive(false);
                _NEXTScreen.SetActive(true);
            }
            
        }
        
    }
}