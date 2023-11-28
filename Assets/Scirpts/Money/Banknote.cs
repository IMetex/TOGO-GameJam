using System;
using Scirpts.Player;
using UnityEngine;

namespace Scirpts.Money
{
    public class Banknote : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.CompareTag("Player"))
            {
                BagController.Instance?.AddBanknoteToBag(this.gameObject);
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<Banknote>().enabled = false;

            }
        }
    }
}