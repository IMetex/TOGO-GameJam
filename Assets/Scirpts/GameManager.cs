using System;
using Cinemachine;
using Scirpts.Enemy;
using Scirpts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Scirpts.Movement;

namespace Scirpts
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject _playScreen;
        public GameObject _mainMenu;
        public GameObject _loseScren;
        public GameObject _NEXTScreen;
        public AudioSource _MUCİS;

        public GameObject _flag;
        public CinemachineVirtualCamera finishCam;
        public PlayerMovement _player;
        
        private void Update()
        {
            NewLevel();
        }

        private void Start()
        {
            _playScreen.SetActive(true);
            _mainMenu.SetActive(false);
            _loseScren.SetActive(false);
            _NEXTScreen.SetActive(false);
        }

        public void PlayButton()
        {
            _playScreen.SetActive(false);
            _mainMenu.SetActive(true);
            _MUCİS.Play();
            
        
        }

        public void RestarBtn()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void NextLevelOne()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
            
                SceneManager.LoadScene(0);
            }
        }

        public void YouLose()
        {
            _mainMenu.SetActive(false);
            _loseScren.SetActive(true);
            _MUCİS.Stop();
            _player._moveSpeed = 0;
        }

        public void NewLevel()
        {
            if (UnitsManager.Instance.enemies.Count == 0)
            {
                finishCam.Priority = 2;
                _mainMenu.SetActive(false);
                _loseScren.SetActive(false);
                _MUCİS.Stop();
                _player._moveSpeed = 0;


                DOTween.Sequence()
                    .SetDelay(1f)
                    .Append(_flag.transform.DOMoveY(-6f, 0.5f))
                    .SetLoops(1, LoopType.Yoyo)
                    .OnComplete(() => ScenePlay());

            }
        }

        private void ScenePlay()
        {
            _NEXTScreen.SetActive(true);
        }
    }
}