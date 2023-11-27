using UnityEngine;

namespace Scirpts.Money
{
    public class BanknoteCreate : MonoBehaviour
    {
        public void CreateBanknote(GameObject _banknote, Transform _creator)
        {
            var banknote = Instantiate(_banknote);
            var position = _creator.position;
            banknote.transform.position = new Vector3(position.x, 0, position.z);
        }
    }
}