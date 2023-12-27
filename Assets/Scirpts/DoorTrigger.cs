using System;
using Cinemachine;
using DG.Tweening;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts
{
    public class DoorTrigger : MonoBehaviour
    {
        public Transform oldDoor;
        public Transform newDoor;
        public CinemachineVirtualCamera newFinishCam;
        public GameObject enemyParent;
        public GameObject levelParent = null;
        public GameObject oldLevel = null;

        private void Start()
        {
            enemyParent.SetActive(false);

            if (levelParent != null)
            {
                levelParent.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemyParent.SetActive(true);
                oldDoor.DOMoveY(2.8f, 0.5f);
                GameManager.Instance.isFinish = false;
                GameManager.Instance.door = newDoor;
                GameManager.Instance.finishCam = newFinishCam;
                UnitsManager.Instance.FindAndAddEnemies();
                
                if (levelParent != null)
                {
                    levelParent.SetActive(true);
                }

                if (oldLevel !=null)
                {
                    oldLevel.SetActive(false);
                   
                }
            }
        }
    }
}