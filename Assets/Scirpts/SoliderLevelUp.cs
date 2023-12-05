using System;
using Scirpts.Money;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Scirpts.Unit;


namespace Scirpts
{
    public class SoliderLevelUp : MonoBehaviour
    {
        [Header("Money Referances")] [SerializeField]
        private int price;

        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text maxText;

        [Space] [Header("Gameobjects Referances")] [SerializeField]
        private GameObject lockedUnit;

        [SerializeField] private GameObject newUnitLvlTwo;
        [SerializeField] private GameObject newUnitLvlThree;


        [Space] [Header("UnitCount Referances")] [SerializeField]
        private int _swpanUnitCount = 3;

        [Header("Partical Referances")] [SerializeField]
        private ParticleSystem _particle;

        private const float DecrementTimerMax = 0.5f;
        private float decrementTimer = DecrementTimerMax;
        private bool isPurchased = false;
        private bool isFirstUnlock = false;

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
            if (other.CompareTag("Player"))
            {
                TryPurchase();
            }
        }

        private void TryPurchase()
        {
            if (!isPurchased && BanknoteManager.Instance.goldBanknoteList.Count > 0)
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

            BanknoteManager.Instance.RemovePlayerBanknoteGold();

            if (price <= 0)
            {
                Unlock();
            }
            else
            {
                priceText.text = price.ToString();
            }
        }

        private void Unlock()
        {
            var friendlyUnitSpawn = GameObject.FindWithTag("UnitSpawn")?.GetComponent<FriendlyUnitSpawn>();
            var baraka = GameObject.FindWithTag("Baraka")?.gameObject;

            if (newUnitLvlTwo != null && !isFirstUnlock)
            {
                friendlyUnitSpawn.SetAlternatePrefab(newUnitLvlTwo);
                FriendlyUnitManager.Instance.MaxUnitCount += _swpanUnitCount;
                _particle.Play();

                price = 5;
                priceText.text = price.ToString();
                isFirstUnlock = true;

                BanknoteManager.Instance.SilverBanknoteTextUpdate(5);

                int childCount = baraka.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform child = baraka.transform.GetChild(i);

                    if (i == 1)
                    {
                        SetVisibility(child.gameObject, true);
                    }
                    else
                    {
                        SetVisibility(child.gameObject, false);
                    }
                }
            }

            else if (isFirstUnlock)
            {
                friendlyUnitSpawn?.SetAlternatePrefab(newUnitLvlThree);
                _particle.Play();
                BanknoteManager.Instance.SilverBanknoteTextUpdate(5);
                int childCount = baraka.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform child = baraka.transform.GetChild(i);

                    if (i == 2)
                    {
                        SetVisibility(child.gameObject, true);
                    }
                    else
                    {
                        SetVisibility(child.gameObject, false);
                    }
                }
            }
        }

        private void SetVisibility(GameObject obj, bool isVisible)
        {
            obj.SetActive(isVisible);
        }
    }
}