using DG.Tweening;
using Scirpts.Money;
using Scirpts.Singleton;
using UnityEngine;

namespace Scirpts.Player
{
    public class BagController : Singleton<BagController>
    {
        [SerializeField] private Transform _bagTransformSilver;
        [SerializeField] private Transform _bagTransformGold = null;
        private Vector3 _banknoteSize;

        private readonly float _banknoteSpacing = 0;
        private readonly int _banknoteIncreaseValue = 1;

        public void AddBanknoteToBag(GameObject banknote)
        {
            banknote.transform.SetParent(_bagTransformSilver, true);
            CalculateObjectSize(banknote);
            float yPos = CalculateNewYPosition();

            SetBanknoteTransform(banknote, yPos);

            BanknoteManager.Instance.BanknoteTextUpdate(_banknoteIncreaseValue);
            BanknoteManager.Instance._playerBanknoteList.Add(banknote);
        }

        private void SetBanknoteTransform(GameObject banknote, float yPos)
        {
            banknote.transform.localRotation = Quaternion.identity;
            banknote.transform.localPosition = new Vector3(0, yPos, 0);
        }

        private float CalculateNewYPosition()
        {
            return _banknoteSize.y * BanknoteManager.Instance._playerBanknoteList.Count +
                   _banknoteSpacing * BanknoteManager.Instance._playerBanknoteList.Count;
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