using System.Collections.Generic;
using Scirpts.Money;
using Scirpts.Singleton;
using Scirpts.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Scirpts.Enemy
{
    public class UnitsManager : Singleton<UnitsManager>
    {
        [Header("Unit List")] public List<Transform> enemies = new List<Transform>();
        public List<Transform> friendlyUnit = new List<Transform>();

        [Header("Progress Bar Enemy")] [SerializeField]
        private ProgressBar _progressBar;

        [Header("Banknote Referance")] [SerializeField]
        private GameObject banknoteSilver;

        [SerializeField] private GameObject banknoteGold;

        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");


        private void Update()
        {
            HandleUnitDeaths(enemies, banknoteGold, false);
            HandleUnitDeaths(friendlyUnit, banknoteSilver, true);
        }

        private void HandleUnitDeaths(List<Transform> units, GameObject banknote, bool isFriendly = false)
        {
            var unitManager = isFriendly ? FriendlyUnitManager.Instance : null;

            for (int i = units.Count - 1; i >= 0; i--)
            {
                var unit = units[i];
                var unitHealth = unit.GetComponent<Stats>().Health;
                var unitAnimator = unit.GetComponent<Animator>();
                var unitBanknote = unit.GetComponent<CreateBanknote>();
                var unitAttack = unit.GetComponent<UnitAttackBase>();

                if (unitHealth <= 0)
                {
                    unitAnimator.SetTrigger(IsDead);
                    
                    units.RemoveAt(i);
                    unitBanknote.BanknoteCreated(unit, banknote);
                    unitAttack.CanAttack = false;
                    unitAttack.Agent.isStopped = true;
                    unitAttack.Agent.velocity = Vector3.zero;
                    unitAttack._isAttacking = false;
                    unitAttack.IsChasing = false;
                    Destroy(unit.gameObject, 1f);
                    
                    if (isFriendly)
                    {
                        unitManager.spawnedUnits.RemoveAt(i);
                        unitManager.points.RemoveAt(i);
                        FriendlyUnitManager.Instance.UnitCountDisplay(-1);
                    }
                    else
                    {
                        _progressBar.UpdateProgressBar();
                    }
                }
            }
        }
    }
}