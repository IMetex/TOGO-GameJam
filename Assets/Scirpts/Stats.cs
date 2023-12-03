using System;
using UnityEngine;

namespace Scirpts
{
    public class Stats : MonoBehaviour
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
            target.GetComponent<Stats>().Health -= Damage;
            
            
        }
    }
}