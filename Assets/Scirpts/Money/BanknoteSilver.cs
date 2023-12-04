using Scirpts.Player;
using UnityEngine;

namespace Scirpts.Money
{
    public class BanknoteSilver : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BagController.Instance.AddSilverBanknoteToBag(this.gameObject);
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<BanknoteSilver>().enabled = false;

            }
        }
    }
}