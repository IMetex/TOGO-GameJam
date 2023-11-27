using System;
using TMPro;
using UnityEngine;

namespace Scirpts
{
    public class LockedUnitController : MonoBehaviour
    {
        [SerializeField] private int price;

        [SerializeField] private TMP_Text priceText;

        [SerializeField] private GameObject lockedUnit;
        [SerializeField] private GameObject unlockedUnit;

        private void Start()
        {
            priceText.text = price.ToString();
        }
        
        
    }
}