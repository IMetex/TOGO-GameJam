using Scirpts.Money;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Scirpts.Unit;


namespace Scirpts
{
    public class NewUnitTake : MonoBehaviour
    {
        [SerializeField] private int price;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private GameObject lockedUnit;
        [SerializeField] private Vector3 _spawnPos;
        [SerializeField] private int _swpanUnitCount = 3;
        [SerializeField] private GameObject newPrefab;
        [SerializeField] private ParticleSystem _particle;

        private const float DecrementTimerMax = 0.5f;
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
        }

        private void Unlock()
        {
            if (lockedUnit != null)
            {
                price = 6;
                priceText.text = price.ToString();
            }

            if (newPrefab != null)
            {
                var friendlyUnitSpawn =GameObject.FindWithTag("UnitSpawn").GetComponent<FriendlyUnitSpawn>();
                friendlyUnitSpawn.SetAlternatePrefab(newPrefab);
                FriendlyUnitManager.Instance.MaxUnitCount += _swpanUnitCount;
                _particle.Play();
            }
            
            isPurchased = true;
        }
    }
}