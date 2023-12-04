using System.Collections.Generic;
using System.Linq;
using Scirpts.Enemy;
using Scirpts.Money;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Scirpts.Unit
{
    public class FriendlyUnitSpawn : MonoBehaviour
    {
        private FormationBase _formation;

        public FormationBase Formation
        {
            get
            {
                if (_formation == null) _formation = GetComponent<FormationBase>();
                return _formation;
            }
            set => _formation = value;
        }

        [SerializeField] private Image fillImage;
        [SerializeField] private float fillSpeed = 0.5f;
        [SerializeField] private BoxFormation boxFormation;
        [SerializeField] private GameObject _unitPrefab;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DecreaseProgressBar();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                boxFormation.UnitWith = FriendlyUnitManager.Instance.spawnedUnits.Count + 1;
            }
        }

        private void DecreaseProgressBar()
        {
            fillImage.fillAmount -= fillSpeed * Time.deltaTime;

            if (fillImage.fillAmount <= 0f)
            {
                if (BanknoteManager.Instance.GetBanknoteCount() > 0 &&
                    FriendlyUnitManager.Instance.spawnedUnits.Count  < FriendlyUnitManager.Instance.MaxUnitCount)
                {
                    FriendlyUnitManager.Instance.points = Formation.EvaluatePoints().ToList();
                    var remainingPoints =
                        FriendlyUnitManager.Instance.points.Skip(FriendlyUnitManager.Instance.spawnedUnits.Count);

                    if (remainingPoints.Any())
                    {
                        Spawn(remainingPoints);
                        boxFormation.UnitWith++;
                        FriendlyUnitManager.Instance.UnitCountDisplay(1);
                        BanknoteManager.Instance.RemovePlayerBanknote();
                        ResetFillBar();
                    }
                }
            }
        }

        private void Spawn(IEnumerable<Vector3> points)
        {
            foreach (var pos in points)
            {
                var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity);
                FriendlyUnitManager.Instance.spawnedUnits.Add(unit);
                UnitsManager.Instance.friendlyUnit.Add(unit.transform);
            }
        }

        private void ResetFillBar()
        {
            fillImage.fillAmount = 1f;
        }
        
        public void SetAlternatePrefab(GameObject alternatePrefab)
        {
            _unitPrefab = alternatePrefab;
        }
    }
}