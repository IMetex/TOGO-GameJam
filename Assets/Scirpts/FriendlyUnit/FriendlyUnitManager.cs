using System.Collections.Generic;
using System.Linq;
using Scirpts.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scirpts.Unit
{
    public class FriendlyUnitManager : Singleton<FriendlyUnitManager>
    {
        public List<GameObject> _spawnedUnits = new List<GameObject>();
        public List<Vector3> _points = new List<Vector3>();
        public int SpawnedUnitsCount { get; set; } = 0;
        public int MaxUnitCount { get; set; } = 5;

        public TMP_Text spawnedUnitText;
        

        public void UnitCountDisplay(int value)
        {
            SpawnedUnitsCount += value;
            spawnedUnitText.text = SpawnedUnitsCount.ToString() + "/ " + MaxUnitCount.ToString();
        }
    }
}