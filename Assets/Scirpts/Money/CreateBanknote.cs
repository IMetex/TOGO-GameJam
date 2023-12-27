using UnityEngine;

namespace Scirpts.Money
{
    public class CreateBanknote : MonoBehaviour
    {
        private readonly Vector3 _spawnPos = new Vector3(0f, 0.2f, 0f);
        
        private int _banknotesCreated = 0;
        public int maxBanknotes;
        
        
        public void BanknoteCreated(Transform unit, GameObject banknote)
        {
            if (_banknotesCreated < maxBanknotes)
            {
                Instantiate(banknote, unit.position + _spawnPos, Quaternion.identity);
                _banknotesCreated++;
            }
        }
    }
}