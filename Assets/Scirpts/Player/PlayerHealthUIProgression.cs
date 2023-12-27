using System;
using Scirpts.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts.Player
{
    public class PlayerHealthUIProgression : MonoBehaviour
    {
        private StatsManager _statsManager;
        private int healtAmount;
        [SerializeField] private Image _healtImg;
        private Vector3 initialPosition;

        private void Awake()
        {
            _statsManager = GetComponent<StatsManager>();
            initialPosition = transform.position;
            
        }

        private void Start()
        {
            healtAmount = _statsManager.Health;
            HealthBarProgress();
        }

        private void Update()
        {
            HealthBarProgress();
        }


        private void HealthBarProgress()
        {
            _healtImg.fillAmount = (float)_statsManager.Health / healtAmount;
        }
    }
}