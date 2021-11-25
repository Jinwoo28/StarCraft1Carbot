using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InputManager;

namespace RTS.Player
{
    public class playerManager : MonoBehaviour
    {

        public static playerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;
        void Start()
        {
            instance = this;
            Units.UnitHandler.instance.SetBasicUnitStats(playerUnits);
            Units.UnitHandler.instance.SetBasicUnitStats(enemyUnits);
        }

        // Update is called once per frame
        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}
