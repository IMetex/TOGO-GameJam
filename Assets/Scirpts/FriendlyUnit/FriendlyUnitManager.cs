using System;
using System.Collections.Generic;
using Scirpts.Singleton;
using TMPro;
using UnityEngine;

namespace Scirpts.Unit
{
    public class FriendlyUnitManager : Singleton<FriendlyUnitManager>
    {
        public List<GameObject> spawnedUnits = new List<GameObject>();
        public List<Vector3> points = new List<Vector3>();
        public int SpawnedUnitsCount { get; set; } = 0;
        public int MaxUnitCount { get; set; } = 0;
        public TMP_Text spawnedUnitText;
        
        public void UnitCountDisplay(int value)
        {
            SpawnedUnitsCount += value;
            spawnedUnitText.text = SpawnedUnitsCount.ToString() + "/" + MaxUnitCount.ToString();
        }

        private void Update()
        {
            spawnedUnitText.text = SpawnedUnitsCount.ToString() + "/" + MaxUnitCount.ToString();
        }
    }
}