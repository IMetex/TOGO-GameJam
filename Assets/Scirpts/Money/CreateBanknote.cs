using UnityEngine;

namespace Scirpts.Money
{
    public class CreateBanknote : MonoBehaviour
    {
        private Vector3 _spwanPos = new Vector3(0f, 0.15f, 0f); 
        private bool isInstantiate = true;
        
        public void BanknoteCreater(Transform unit, GameObject banknote)
        {
            if (isInstantiate)
            {
                Instantiate(banknote, unit.position + _spwanPos, Quaternion.identity);
                isInstantiate = false;
            }
        }
    }
}