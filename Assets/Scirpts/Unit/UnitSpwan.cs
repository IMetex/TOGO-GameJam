using System;
using System.Collections.Generic;
using System.Linq;
using Scirpts.Animation;
using Scirpts.Money;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

        private Transform unitOffsetRef;
        private Transform _playerRotationRef;
        private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
        private List<Vector3> _points = new List<Vector3>();
        private Transform _parent;

        private void Start()
        {
            GameObject unitOffsetObject = GameObject.FindWithTag("UnitOffsetTag");
            if (unitOffsetObject != null)
            {
                unitOffsetRef = unitOffsetObject.transform;
            }

            GameObject playerRotationObject = GameObject.FindWithTag("PlayerRotationTag");
            if (playerRotationObject != null)
            {
                _playerRotationRef = playerRotationObject.transform;
            }
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
            boxFormation.UnitDepth = 1;
        }

        private void OnTriggerExit(Collider other)
        {
            boxFormation.UnitDepth = 2;
        }


        private void FillProgressBar()
        {
            fillImage.fillAmount += fillSpeed * Time.deltaTime;

            if (fillImage.fillAmount >= 1f && BanknoteManager.Instance != null &&
                BanknoteManager.Instance.GetBanknoteCount() > 0)
            {
                _points = Formation.EvaluatePoints().ToList();
                var remainingPoints = _points.Skip(_spawnedUnits.Count);
                Spawn(remainingPoints);
                boxFormation.UnitWith++;
                BanknoteManager.Instance.RemovePlayerBanknote();
                ResetFillBar();
            }
        }

        private void Update()
        {
            SetFormation();
        }


        public void SetFormation()
        {
            _points = Formation.EvaluatePoints().ToList();

            for (var i = 0; i < _spawnedUnits.Count; i++)
            {
                GameObject unit = _spawnedUnits[i];
                Animator unitAnimator = unit.GetComponent<Animator>();

                Vector3 targetPosition = unitOffsetRef.position + _points[i];


                unit.transform.position =
                    Vector3.MoveTowards(unit.transform.position, targetPosition, _unitSpeed * Time.deltaTime);

                if (Vector3.Distance(unit.transform.position, targetPosition) > 0.1f)
                {
                    unitAnimator.SetBool("IsWalking", true);
                }
                else
                {
                    unitAnimator.SetBool("IsWalking", false);
                }

                Vector3 direction = (_playerRotationRef.position - unit.transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    float rotationSpeed = 90f;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation,
                        rotationSpeed * Time.deltaTime);
                }
            }
        }

        private void ResetFillBar()
        {
            fillImage.fillAmount = 0f;
        }

        private void Spawn(IEnumerable<Vector3> points)
        {
            foreach (var pos in points)
            {
                var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
                _spawnedUnits.Add(unit);
            }
        }

        private void Kill(int num)
        {
            for (var i = 0; i < num; i++)
            {
                var unit = _spawnedUnits.Last();
                _spawnedUnits.Remove(unit);
                Destroy(unit.gameObject);
            }
        }
    }
}