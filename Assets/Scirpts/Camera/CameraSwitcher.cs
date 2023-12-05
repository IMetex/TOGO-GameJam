using System;
using Cinemachine;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        private const string PlayerFollowTag = "Player";
        private Transform _player;
        public Animator animator;

        public int distanceRange = 10;

        public CinemachineVirtualCamera mainCam;
        public CinemachineVirtualCamera warCam;
        public CinemachineVirtualCamera finishCam;

        private void Start()
        {
            mainCam.Priority = 1;
            _player = GameObject.FindWithTag(PlayerFollowTag)?.transform;

        }
        private void Update()
        {
           PlayerCameraChange();
           //FinishCam();
        }

        private void PlayerCameraChange()
        {
            bool enemyInRange = false;

            foreach (var enemy in UnitsManager.Instance.enemies)
            {
                float distanceToEnemy = Vector3.Distance(enemy.position, _player.position + Vector3.up);

                if (distanceToEnemy <= distanceRange)
                {
                    enemyInRange = true;
                    break;
                }
            }

            if (enemyInRange)
            {
                animator.Play("WarCamera");
                mainCam.Priority = 0;
                warCam.Priority = 1;
            }
            else
            {
                animator.Play("MainCamera");
                warCam.Priority = 0;
                mainCam.Priority = 1;
            }
        }

        private void FinishCam()
        {
            if (UnitsManager.Instance.enemies.Count == 0)
            {
                animator.Play("FinishCam");
                finishCam.Priority = 2;
            }
        }
    }
}