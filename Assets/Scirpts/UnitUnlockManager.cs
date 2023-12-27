using Scirpts.Money;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Scirpts.Unit;
using UnityEngine.Serialization;


namespace Scirpts
{
    public class UnitUnlockManager : MonoBehaviour
    {
        [Header("Money Referance")] [SerializeField]
        private int price;

        [SerializeField] private TMP_Text priceText;

        [Header("Gameobject Referance")] [SerializeField]
        private GameObject destroyObject;
        [SerializeField] private GameObject instantiateObject;
        
        [Header("Unit Spawn Count")]
        [SerializeField] private int _swpanUnitCount = 5;

        [Space] [SerializeField] private Vector3 _spawnPos;

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
            if (!isPurchased && BanknoteManager.Instance.silverBanknoteList.Count > 0)
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
            if (destroyObject != null)
            {
                Destroy(destroyObject);
            }

            if (instantiateObject != null)
            {
                Quaternion rotation = Quaternion.Euler(0, -90, 0);
                var unlockObject = Instantiate(instantiateObject, transform.position + _spawnPos, rotation);
                unlockObject.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
                FriendlyUnitManager.Instance.MaxUnitCount += _swpanUnitCount;
            }
            
            isPurchased = true;
        }
    }
}