using System;
using System.Linq;
using Scirpts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Scirpts
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;
        private int progress = 0;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = UnitsManager.Instance.enemies.Count;
        }

        public void UpdateProgressBar()
        {
            progress++;
            _slider.value = progress;
        }


        public void ResetProgressBar()
        {
            _slider.value = 0;
        }
    }
}