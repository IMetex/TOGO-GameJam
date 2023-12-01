using System.Collections.Generic;
using Scirpts.Singleton;
using TMPro;
using UnityEngine;

namespace Scirpts.Money
{
    public class BanknoteManager : Singleton<BanknoteManager>
    {
        public List<GameObject> _playerBanknoteList = new List<GameObject>();
        private int _banknoteCount = 0;
        public TMP_Text _banknoteText;
        private readonly int _banknoteIncreaseValue = 1;

        public int BanknoteCount
        {
            get => _banknoteCount;
            set => _banknoteCount = value;
        }

        public void BanknoteTextUpdate(int value)
        {
            _banknoteCount += value;
            _banknoteText.text = _banknoteCount.ToString();
        }

        public int GetBanknoteCount()
        {
            return _banknoteCount;
        }

        public void SpendBanknoteCount(int price)
        {
            _banknoteCount -= price;
        }

        private bool TryBuyThisUnit(int price)
        {
            if (BanknoteManager.Instance.GetBanknoteCount() >= price)
            {
                BanknoteManager.Instance.SpendBanknoteCount(price);
                return true;
            }

            return false;
        }

        public void RemovePlayerBanknote()
        {
            int lastIndex = _playerBanknoteList.Count - 1;
            
            if (lastIndex >= 0)
            {
                GameObject banknoteToRemove = _playerBanknoteList[lastIndex];
                _playerBanknoteList.RemoveAt(lastIndex);
                BanknoteTextUpdate(-_banknoteIncreaseValue);
                Destroy(banknoteToRemove);
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