using System;
using System.Collections.Generic;
using System.Linq;
using Scirpts.Enemy;
using Scirpts.Money;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Scirpts.Unit
{
    public class UnitSpwan : MonoBehaviour
    {
        [Header("UI Fill Referances")] [SerializeField]
        private Image fillImage;

        [SerializeField] private float fillSpeed = 0.5f;

        [SerializeField] private BoxFormation boxFormation;

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

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private float _unitSpeed = 2;
        [SerializeField] private GameObject _tent;

        private Transform unitOffsetRef = null;
        private Transform _playerRotationRef = null;


        private void Start()
        {
            FindGameObjectsWithTag();
        }

        private void FindGameObjectsWithTag()
        {
            unitOffsetRef = GameObject.FindGameObjectWithTag("UnitOffsetTag")?.transform;
            _playerRotationRef = GameObject.FindGameObjectWithTag("PlayerRotationTag")?.transform;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                FillProgressBar();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                boxFormation.UnitDepth = 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                boxFormation.UnitDepth = 2;
            }
        }


        private void FillProgressBar()
        {
            fillImage.fillAmount += fillSpeed * Time.deltaTime;

            if (fillImage.fillAmount >= 1f && BanknoteManager.Instance.GetBanknoteCount() > 0 &&
                UnitManager.Instance.SpawnedUnitsCount < UnitManager.Instance.MaxUnitCount)
            {
                UnitManager.Instance._points = Formation.EvaluatePoints().ToList();
                var remainingPoints =
                    UnitManager.Instance._points.Skip(UnitManager.Instance._spawnedUnits.Count);
                Spawn(remainingPoints);
                
                ObjectTweenAnim(_tent);
                boxFormation.UnitWith++;
                UnitManager.Instance.UnitCountDisplay();
                BanknoteManager.Instance.RemovePlayerBanknote();
                ResetFillBar();
            }
        }

        private void Spawn(IEnumerable<Vector3> points)
        {
            foreach (var pos in points)
            {
                var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity);
                UnitManager.Instance._spawnedUnits.Add(unit);
                EnemyManager.Instance.friendlyUnit.Add(unit.transform);
            }
        }

        private void Update()
        {
            SetFormation();
        }

        public void SetFormation()
        {
            UnitManager.Instance._points = Formation.EvaluatePoints().ToList();

            for (var i = 0; i < UnitManager.Instance._spawnedUnits.Count; i++)
            {
                MoveUnit(UnitManager.Instance._spawnedUnits[i], UnitManager.Instance._points[i]);
            }
        }

        private void MoveUnit(GameObject unit, Vector3 targetPosition)
        {
            Animator unitAnimator = unit.GetComponent<Animator>();

            unit.transform.position = Vector3.MoveTowards(unit.transform.position,
                unitOffsetRef.position + targetPosition, _unitSpeed * Time.deltaTime);

            bool move = Vector3.Distance(unit.transform.position, unitOffsetRef.position + targetPosition) > 0.1f;
            unitAnimator.SetBool("IsWalking", move);

            RotateUnitTowardsPlayer(unit);
        }

        private void RotateUnitTowardsPlayer(GameObject unit)
        {
            Vector3 direction = (_playerRotationRef.position - unit.transform.position).normalized;

            if (direction != Vector3.zero)
            {
                float rotationSpeed = 90f;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }

        private void ResetFillBar()
        {
            fillImage.fillAmount = 0f;
        }

        private void ObjectTweenAnim(GameObject obj)
        {
            var scale = new Vector3(0.5f, 0.5f, 0.5f);
            Vector3 doScale = scale * 1.1f;
            
            obj.transform.DOScale(doScale, 0.05f).OnComplete(() =>
                obj.transform.DOScale(scale, 0.05f));

        }
    }
}