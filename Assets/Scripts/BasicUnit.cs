using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic", order = 1)]

    public class BasicUnit : ScriptableObject
    {

        public enum unitType
        {
            Worker,
            Warrior,
            Healer

        };
        //public bool isPlayerUnit;
        [Space(15)]
        [Header("Unit Settings")]
        public unitType type;

        public new string name;

        public GameObject unitPrefab;
        //public GameObject enemyPrefab;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(15)]
        public int cost;
        public int attack;
        public int atkRange;
        public int health;
        public int armor;


    }
}
