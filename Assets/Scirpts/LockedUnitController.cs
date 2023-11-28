using System;
using Scirpts.Money;
using Scirpts.Player;
using TMPro;
using UnityEngine;


namespace Scirpts
{
    public class LockedUnitController : MonoBehaviour
    {
        [SerializeField] private int price;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private GameObject lockedUnit;
        [SerializeField] private GameObject unlockedUnit;

        private const float DecrementTimerMax = 2f;
        private float decrementTimer = DecrementTimerMax;

        private bool isPurchased = false;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            priceText.text = price.ToString();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                TryPurchase();
            }
        }

        private void TryPurchase()
        {
            if (!isPurchased && BanknoteManager.Instance._playerBanknoteList.Count > 0)
            {
                decrementTimer -= Time.deltaTime;

                if (decrementTimer <= 0f)
                {
                    decrementTimer = DecrementTimerMax;
                    Purchase();
                }
            }
        }

        private void Purchase()
        {
            price--;
            priceText.text = price.ToString();

            BanknoteManager.Instance.RemovePlayerBanknote();

            if (price <= 0)
            {
                Unlock();
            }
        }

        private void Unlock()
        {
            if (lockedUnit != null)
            {
                Destroy(lockedUnit);
            }

            if (unlockedUnit != null)
            {
                Instantiate(unlockedUnit);
            }

            isPurchased = true;
        }
    }
}