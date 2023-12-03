using DG.Tweening;
using Scirpts.Money;
using Scirpts.Singleton;
using UnityEngine;

namespace Scirpts.Player
{
    public class BagController : Singleton<BagController>
    {
        [SerializeField] private Transform _bagTransformSilver;
        [SerializeField] private Transform _bagTransformGold;
        private Vector3 _banknoteSize;

        private readonly float _banknoteSpacing = 0;
        private readonly int _banknoteIncreaseValue = 1;

        public void AddSilverBanknoteToBag(GameObject banknote)
        {
            banknote.transform.SetParent(_bagTransformSilver, true);

            CalculateObjectSize(banknote);
            float yPos = CalculateNewYPositionGold() + CalculateNewYPosition();

            SetBanknoteTransform(banknote, yPos);

            BanknoteManager.Instance.SilverBanknoteTextUpdate(_banknoteIncreaseValue);
            BanknoteManager.Instance.silverBanknoteList.Add(banknote);
        }

        public void AddGoldBanknoteToBag(GameObject banknote)
        {
            banknote.transform.SetParent(_bagTransformGold, true);

            CalculateObjectSize(banknote);
            float yPos = CalculateNewYPosition() + CalculateNewYPositionGold();
            SetBanknoteTransform(banknote, yPos);

            BanknoteManager.Instance.GoldBanknoteTextUpdate(_banknoteIncreaseValue);
            BanknoteManager.Instance.goldBanknoteList.Add(banknote);
        }

        private void SetBanknoteTransform(GameObject banknote, float yPos)
        {
            banknote.transform.localRotation = Quaternion.identity;
            banknote.transform.localPosition = new Vector3(0, yPos, 0);
        }

        private float CalculateNewYPosition()
        {
            float totalHeight = (_banknoteSize.y + _banknoteSpacing) * BanknoteManager.Instance.goldBanknoteList.Count;
            return totalHeight;
        }

        private float CalculateNewYPositionGold()
        {
            float totalHeight = (_banknoteSize.y + _banknoteSpacing) *
                                BanknoteManager.Instance.silverBanknoteList.Count;
            return totalHeight;
        }

        private void CalculateObjectSize(GameObject gameObject)
        {
            if (_banknoteSize == Vector3.zero)
            {
                MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
                _banknoteSize = meshRenderer.bounds.size;
            }
        }
    }
}