using System;
using UnityEngine;

namespace Scirpts.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = UnitsManager.Instance.enemies[i];
                    var enemyNavmesh = enemy.GetComponent<UnitControllers>();

                    if (enemyNavmesh != null)
                    {
                        enemyNavmesh.chaseDistance = 2;
                    }
                    else
                    {
                        Debug.LogError("UnitControllers component not found on enemy object.");
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = UnitsManager.Instance.enemies[i];
                    var enemyNavmesh = enemy.GetComponent<UnitControllers>();

                    if (enemyNavmesh != null)
                    {
                        enemyNavmesh.chaseDistance = 15;
                    }
                    else
                    {
                        Debug.LogError("UnitControllers component not found on enemy object.");
                    }
                }
            }
        }
    }
}