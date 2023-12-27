using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Scirpts
{
    public class FormationCam : MonoBehaviour
    {
        public CinemachineVirtualCamera formationCam;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                formationCam.Priority = 3;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                formationCam.Priority = -3;
            }
        }
    }
}