using System;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerAddHeal : MonoBehaviour
    {
        private StatsManager _statsManager;
        public int plusHealth;
        private int currentHealth;
        public ParticleSystem _healParticle;

        private void Start()
        {
            _statsManager = GetComponent<StatsManager>();
            currentHealth = _statsManager.Health;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Heal"))
            {
                _statsManager.Health += plusHealth;
                _healParticle.Play();
                Destroy(other.gameObject);
            }
        }
    }
}