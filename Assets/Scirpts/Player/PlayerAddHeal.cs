using System;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerAddHeal : MonoBehaviour
    {
        private Stats _stats;
        public int plusHealth;

        private void Start()
        {
            _stats = GetComponent<Stats>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Heal"))
            {
                int currentHealth = _stats.Health;

                if (currentHealth < _stats.Health)
                {
                    int newHealth = Mathf.Min(currentHealth + plusHealth, _stats.Health);
                    _stats.Health = newHealth;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}