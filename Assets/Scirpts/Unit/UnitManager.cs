using System.Collections.Generic;
using System.Linq;
using Scirpts.Singleton;
using TMPro;
using UnityEngine;

namespace Scirpts.Unit
{
    public class UnitManager : Singleton<UnitManager>
    {
        public List<GameObject> _spawnedUnits = new List<GameObject>();
        public List<Vector3> _points = new List<Vector3>();
        public int SpawnedUnitsCount { get; set; } = 0;
        public int MaxUnitCount { get; set; } = 0;
        
        public TMP_Text _spawnedUnitText;

        public void Kill(int num)
        {
            for (var i = 0; i < num; i++)
            {
                var unit = _spawnedUnits.Last();
                _spawnedUnits.Remove(unit);
                Destroy(unit.gameObject);
            }
        }

        public void UnitCountDisplay()
        {
            SpawnedUnitsCount++;
            _spawnedUnitText.text = SpawnedUnitsCount.ToString() + "/ " + MaxUnitCount.ToString();
        }
    }
}