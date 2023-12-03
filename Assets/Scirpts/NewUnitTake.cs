using System;
using TMPro;
using UnityEngine;

namespace Scirpts
{
    public class NewUnitTake : MonoBehaviour
    {
        [SerializeField] private int price;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private GameObject lockedUnit;
        [SerializeField] private GameObject unlockedUnit;
        [SerializeField] private Vector3 _spawnPos;
        [SerializeField] private int _swpanUnitCount = 5;

        private void OnTriggerStay(Collider other)
        {
            
        }
    }
}
