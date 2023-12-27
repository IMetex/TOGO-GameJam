using System;
using UnityEngine;

namespace Scirpts.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        public int _newchaseDistance = 8;

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
                        enemyNavmesh.chaseDistance -= enemyNavmesh.chaseDistance - 1;
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
                        enemyNavmesh.chaseDistance = _newchaseDistance;
                    }
                }
            }
        }
    }
}