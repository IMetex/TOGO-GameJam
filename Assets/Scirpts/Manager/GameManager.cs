using System;
using System.Collections;
using Cinemachine;
using Scirpts.Enemy;
using Scirpts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Scirpts.Movement;
using TMPro;
using UnityEngine.Serialization;

namespace Scirpts
{
    public class GameManager : Singleton<GameManager>
    {
        public ProgressBar progressBar;
        [Header("UI Reference")] public GameObject playScreen;
        public GameObject mainMenu;
        public GameObject loseScreen;
        public GameObject nextScreen;

        public Transform door;

        
        [Header("Audio")] public AudioSource music;

        [Header("Camera")] public CinemachineVirtualCamera finishCam;
        public PlayerMovement _player;
        public TMP_Text _levelText;
        public int levelCount = 1;
        public bool isFinish = false;

        public GameObject joystick;

        private void Update()
        {
            NextSession();
            LevelTextUpdate();
        }

        private void LevelTextUpdate()
        {
            _levelText.text = "Level " + levelCount.ToString();
        }

        private void Start()
        {
            playScreen.SetActive(true);
            mainMenu.SetActive(false);
            loseScreen.SetActive(false);
            nextScreen.SetActive(false);
        }

        public void PlayButton()
        {
            playScreen.SetActive(false);
            mainMenu.SetActive(true);
            music.Play();
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
            mainMenu.SetActive(false);
            loseScreen.SetActive(true);
            music.Stop();
            _player._moveSpeed = 0;
        }

        public void NextSession()
        {
            if (UnitsManager.Instance.enemies.Count == 0)
            {
                if (!isFinish)
                {
                    mainMenu.SetActive(false);
                    loseScreen.SetActive(false);
                    nextScreen.SetActive(true);
                    _player._moveSpeed = 0;
                    joystick.SetActive(false);
                    finishCam.Priority = 2;
                    music.Stop();
                    isFinish = true;
                    levelCount++;
                    progressBar.ResetProgressBar();
                }

                DOTween.Sequence()
                    .SetDelay(0.5f)
                    .Append(door.transform.DOMoveY(6f, 1f))
                    .SetDelay(2f)
                    .OnComplete(() => ScenePlay());
            }
        }


        private void ScenePlay()
        {
            finishCam.Priority = -2;
            music.Play();
            mainMenu.SetActive(true);
            nextScreen.SetActive(false);
            _player._moveSpeed = 5;
            joystick.SetActive(true);
        }
    }
}