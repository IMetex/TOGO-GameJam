using System;
using Scirpts.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts.Player
{
    public class PlayerHealthUIProgression : MonoBehaviour
    {
        private Stats _stats;
        private int healtAmount;
        [SerializeField] private Image _healtImg;


        private void Awake()
        {
            _stats = GetComponent<Stats>();
        }

        private void Start()
        {
            healtAmount = _stats.Health;
            HealthBarProgress();
        }

        private void Update()
        {
            HealthBarProgress();

            if (_stats.Health < 0)
            {
                GameManager.Instance.YouLose();
            }
        }


        private void HealthBarProgress()
        {
            _healtImg.fillAmount = (float)_stats.Health / healtAmount;
        }
    }
}