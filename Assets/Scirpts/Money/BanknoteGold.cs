using System;
using Scirpts.Player;
using UnityEngine;

namespace Scirpts.Money
{
    public class BanknoteGold : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BagController.Instance.AddGoldBanknoteToBag(this.gameObject);
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<BanknoteGold>().enabled = false;
                audioSource.Play();
            }
        }
    }
}