using System;
using UnityEngine;
using DG.Tweening;

namespace Scirpts.Money
{
    public class BanknoteCreate : MonoBehaviour
    {
        [SerializeField] private GameObject _banknoteGold;
        [SerializeField] private Vector3 pos;

        private Stats _stats;

        private void Start()
        {
            _stats = GetComponent<Stats>();
        }

        private void Update()
        {
            if (_stats._health <= 0)
            {
                var banknoteGold = Instantiate(_banknoteGold, transform.position + pos, Quaternion.identity);
            }
        }

        public void CreateBanknote(GameObject _banknote, Transform _creator)
        {
            var banknote = Instantiate(_banknote);
            var position = _creator.position;
            banknote.transform.position = new Vector3(position.x, 0, position.z);
        }
    }
}