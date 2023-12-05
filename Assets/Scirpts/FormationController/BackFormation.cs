using Scirpts.Formations.Scripts;
using UnityEngine;

namespace Scirpts
{
    public class BackFormation : MonoBehaviour
    {
        public int unitDepth = 2;
        public float spread = 1.5f;
        public float ntfOfsset = -0.3f;

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