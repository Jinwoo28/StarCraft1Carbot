using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        public int cost, attack, atkRange, health, armor;

        public float speed;

        private Camera camera;

        private Vector3 destination;
        private bool isMove;

        private void Awake()
        {
            camera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveUnit();
        }
        public void SetDestinatin(Vector3 dest)
        {
            destination = dest;
            isMove = true;
        }
        public void MoveUnit() 
        {
            if (isMove)
            {
                var dir = destination - transform.position;
                transform.position += dir.normalized * Time.deltaTime * speed;
            }
            if (Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                isMove = false;
                
            }
        }

    }
}