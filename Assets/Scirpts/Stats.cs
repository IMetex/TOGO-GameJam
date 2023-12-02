using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int _health;
    public int damage;


    public void TakeDamage(GameObject target, int damaged)
    {
        target.GetComponent<Stats>()._health -= damaged;
    }
}