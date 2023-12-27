using System;
using Scirpts.Money;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Scirpts.Unit;
using UnityEngine.Serialization;


namespace Scirpts
{
    public class UnitLevelUp : MonoBehaviour
    {
        public FriendlyUnitSpawn friendlyUnitSpawn;
        [Header("Money Referances")] [SerializeField]
        private int price;

        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text maxText;

        [Space] [Header("Gameobjects Referances")] [SerializeField]
        private GameObject lockedUnit;

        [SerializeField] private GameObject newUnitLvlTwo;
        [SerializeField] private GameObject newUnitLvlThree;

        [Header("Baraka Visibilty")] [SerializeField]
        private GameObject _baraka;

        [Space] [Header("UnitCount Referances")] [SerializeField]
        private int _swpanUnitCount = 3;

        [Header("Partical Referances")] [SerializeField]
        private ParticleSystem _lvlUpParticle;
        [SerializeField]
        private ParticleSystem _smokeParticle;

        private const float DecrementTimerMax = 0.5f;
        private float decrementTimer = DecrementTimerMax;
        private bool isPurchased = false;
        private bool isFirstUnlock = false;
        private bool isMaxLevel = false;

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
            if (!isMaxLevel)
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
        }

        private void Unlock()
        {

            if (newUnitLvlTwo != null && !isFirstUnlock)
            {
                UnlockUnit(newUnitLvlTwo, 1);
                price = 5;
                priceText.text = price.ToString();
                _smokeParticle.Play();
            }
            else if (isFirstUnlock)
            {
                UnlockUnit(newUnitLvlThree, 2);
                priceText.gameObject.SetActive(false);
                maxText.text = "Max Level";
                _smokeParticle.Play();
                isMaxLevel = true;
            }
        }

        private void UnlockUnit(GameObject unitPrefab, int visibilityIndex)
        {
            friendlyUnitSpawn?.SetAlternatePrefab(unitPrefab);
            FriendlyUnitManager.Instance.MaxUnitCount += _swpanUnitCount;
            SetVisibility(_baraka, visibilityIndex);
            isFirstUnlock = true;
            _lvlUpParticle.Play();
        }


        private void SetVisibility(GameObject obj, int visibleIndex)
        {
            int childCount = obj.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = obj.transform.GetChild(i);
                SetVisibility(child.gameObject, i == visibleIndex);
            }
        }

        private void SetVisibility(GameObject obj, bool isVisible)
        {
            obj.SetActive(isVisible);
        }
    }
}