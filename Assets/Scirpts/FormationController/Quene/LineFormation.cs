using System;
using Scirpts.Formations.Scripts;
using UnityEngine;

namespace Scirpts
{
    public class LineFormation : MonoBehaviour
    {
        public int unitDepth = 1;
        public float spread = 1.8f;
        public float ntfOfsset = 0f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BoxFormation.Instance.UnitDepth = unitDepth;
                BoxFormation.Instance.Spread = spread;
                BoxFormation.Instance._nthOffset = ntfOfsset;
            }
        }
    }
}
