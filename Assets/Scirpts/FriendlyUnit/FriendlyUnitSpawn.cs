using System;
using System.Collections.Generic;
using System.Linq;
using Scirpts.Enemy;
using Scirpts.Money;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Scirpts.Formations.Scripts;

namespace Scirpts.Unit
{
    public class FriendlyUnitSpawn : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private float fillSpeed = 0.5f;
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private GameObject _barakaPrefab;
       
        private const string PlayerFollowTag = "PlayerFollow";
        
        private Transform _playerFollow;

        private void Start()
        {
            _playerFollow = GameObject.FindWithTag(PlayerFollowTag)?.transform;
        }

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
                BoxFormation.Instance.UnitWith = FriendlyUnitManager.Instance.spawnedUnits.Count + 1;
                BoxFormation.Instance.UnitDepth = 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BoxFormation.Instance.UnitDepth = 2;
            }
        }

        private void DecreaseProgressBar()
        {
            fillImage.fillAmount -= fillSpeed * Time.deltaTime;

            if (fillImage.fillAmount <= 0f)
            {
                if (BanknoteManager.Instance.GetBanknoteCount() > 0 &&
                    FriendlyUnitManager.Instance.spawnedUnits.Count < FriendlyUnitManager.Instance.MaxUnitCount)
                {
                    FriendlyUnitManager.Instance.points = BoxFormation.Instance.EvaluatePoints().ToList();
                    var remainingPoints =
                        FriendlyUnitManager.Instance.points.Skip(FriendlyUnitManager.Instance.spawnedUnits.Count);

                    Spawn(remainingPoints);
                    BoxFormation.Instance.UnitWith++;
                    FriendlyUnitManager.Instance.UnitCountDisplay(1);
                    BanknoteManager.Instance.RemovePlayerBanknote();
                    TakeTweenAnimaton(_barakaPrefab);
                    ResetFillBar();
                }
            }
        }
        
        private void Spawn(IEnumerable<Vector3> points)
        {
            foreach (var pos in points)
            {
                var spawnPosition = transform.position + pos;
                var unit = Instantiate(_unitPrefab, spawnPosition, Quaternion.identity);
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

        public void TakeTweenAnimaton(GameObject obj)
        {
            var scale = new Vector3(0.7f, 0.7f, 0.7f);
            Vector3 doScale = scale * 1.2f;

            obj.transform.DOScale(doScale, 0.06f).OnComplete(() =>
                obj.transform.DOScale(scale, 0.06f));
        }
    }
}