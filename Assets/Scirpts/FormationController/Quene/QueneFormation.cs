using Scirpts.Formations.Scripts;
using UnityEngine;

namespace Scirpts
{
    public class QueneFormation : MonoBehaviour
    {
        public int unitDepth = 3;
        public float spread = 1f;
        public float ntfOfsset = 0.5f;

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