using System.Collections.Generic;
using DG.Tweening;
using Scirpts.Singleton;
using UnityEngine;

namespace Scirpts.Formations.Scripts
{
    public class BoxFormation : Singleton<BoxFormation>
    {
        [SerializeField] private float Spread = 1;
        [SerializeField] private int _unitWidth = 5;
        [SerializeField] private int _unitDepth = 5;
        [SerializeField] private float _nthOffset = 0;

        public int UnitWith
        {
            get => _unitWidth;
            set => _unitWidth = value;
        }

        public int UnitDepth
        {
            get => _unitDepth;
            set => _unitDepth = value;
        }

        public IEnumerable<Vector3> EvaluatePoints()
        {
            var middleOffset = new Vector3(_unitWidth * 0.5f, 0, _unitDepth * 0.5f);

            for (var x = 0; x < _unitWidth; x++)
            {
                for (var z = 0; z < _unitDepth; z++)
                {
                    var pos = new Vector3(x + (z % 2 == 0 ? 0 : _nthOffset), 0, z);
                    pos -= middleOffset;
                    pos *= Spread;
                    yield return pos;
                }
            }
        }
    }
}