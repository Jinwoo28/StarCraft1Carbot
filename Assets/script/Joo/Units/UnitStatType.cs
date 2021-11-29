using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class UnitStatType : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, aggroRange, atkRange,atkspeed, attack, health, armor;
        }
    }
}
