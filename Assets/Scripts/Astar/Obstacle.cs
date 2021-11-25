using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
//            for (int i = 0; i < this.transform.position.x; i++)
//            {
                Grid.gridinstance.ObstacleNode(other.transform.position);
            Debug.Log("厘局拱 积己");
  //          }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            //            for (int i = 0; i < this.transform.position.x; i++)
            //            {
            Grid.gridinstance.ExitObstacleNode(other.transform.position);
            Debug.Log("厘局拱 力芭");
            //          }
        }
    }
}
