using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BuildingStatType : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float health, armor, attack;
        }
    }
}
