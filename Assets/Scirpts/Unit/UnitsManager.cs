using System.Collections.Generic;
using Scirpts.Singleton;
using UnityEngine;

namespace Scirpts.Enemy
{
    public class UnitsManager : Singleton<UnitsManager>
    {
        public List<Transform> enemies = new List<Transform>();
        public List<Transform> friendlyUnit = new List<Transform>();
    
    }
}
