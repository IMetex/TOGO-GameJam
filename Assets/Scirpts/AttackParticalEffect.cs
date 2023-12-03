using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticalEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particalAttack;
    
    void AttackPartical(int attack)
    {
        if (attack == 1)
        {
            particalAttack.Play();
        }
    }
}
