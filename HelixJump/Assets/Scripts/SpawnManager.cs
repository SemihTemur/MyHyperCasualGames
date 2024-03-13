using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cylinder;
    


  public void Spawn()
    {
        if (!Ball.gameover)
        {
            for(int i = 0; i < 3; i++)
            {
                cylinder = Instantiate(cylinder, cylinder.transform.position + new Vector3(0, -26, 0), cylinder.transform.rotation);
            }
           
        }     
        
    }

    
     
    
}
