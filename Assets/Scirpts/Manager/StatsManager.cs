using System;
using UnityEngine;

namespace Scirpts
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int damage;
        public int Health
        {
            get => health;
            set => health = value;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }


        public void TakeDamage(GameObject target, int damaged)
        {
            target.GetComponent<StatsManager>().Health -= Damage;
            
            
        }
    }
}