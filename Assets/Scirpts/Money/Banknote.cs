using System;
using UnityEngine;

namespace Scirpts.Money
{
    public class Banknote : MonoBehaviour
    {
        private BagController _bagController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                _bagController = other.GetComponent<BagController>();
                _bagController.AddBanknoteToBag(this.gameObject);
            }
        }
    }
}