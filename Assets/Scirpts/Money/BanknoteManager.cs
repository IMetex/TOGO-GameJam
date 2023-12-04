using System.Collections.Generic;
using Scirpts.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scirpts.Money
{
    public class BanknoteManager : Singleton<BanknoteManager>
    {
        public List<GameObject> silverBanknoteList = new List<GameObject>();
        public List<GameObject> goldBanknoteList = new List<GameObject>();

        private int _goldBanknoteCount = 0;

        [SerializeField] private TMP_Text _silverBanknoteText;
        [SerializeField] private TMP_Text _goldBanknoteText;
        private readonly int _banknoteIncreaseValue = 1;

        public int SilverBanknoteCount { get; set; } = 0;

        public int GoldBanknoteCount { get; set; } = 0;


        public void SilverBanknoteTextUpdate(int value)
        {
            SilverBanknoteCount += value;
            _silverBanknoteText.text = SilverBanknoteCount.ToString();
        }

        public void GoldBanknoteTextUpdate(int value)
        {
            GoldBanknoteCount += value;
            _goldBanknoteText.text = GoldBanknoteCount.ToString();
        }

        public int GetBanknoteCount()
        {
            return SilverBanknoteCount;
        }

        public void SpendBanknoteCount(int price)
        {
            SilverBanknoteCount -= price;
        }

        private bool TryBuyThisUnit(int price)
        {
            if (GetBanknoteCount() >= price)
            {
                SpendBanknoteCount(price);
                return true;
            }

            return false;
        }

        public void RemovePlayerBanknote()
        {
            int lastIndex = silverBanknoteList.Count - 1;

            if (lastIndex >= 0)
            {
                GameObject banknoteToRemove = silverBanknoteList[lastIndex];
                silverBanknoteList.RemoveAt(lastIndex);
                SilverBanknoteTextUpdate(-_banknoteIncreaseValue);
                Destroy(banknoteToRemove);
            }
        }

        public void RemovePlayerBanknoteGold()
        {
            int lastIndex = goldBanknoteList.Count - 1;

            if (lastIndex >= 0)
            {
                GameObject banknoteToRemove = goldBanknoteList[lastIndex];
                goldBanknoteList.RemoveAt(lastIndex);
                GoldBanknoteTextUpdate(-_banknoteIncreaseValue);
                Destroy(banknoteToRemove);
            }
        }
    }
}

