using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;

    public bool requiresKey;
    public bool reqkey1;

    public GameObject areaToSpawn;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //code for key to open door
            if (requiresKey)
            {
                //do additional checks
                if(reqkey1 && other.GetComponent<PlayerInventory>().hasKey1)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");
                    //spawns enemies in area
                    //areaToSpawn.SetActive(true);
                }
            }
            else
            {
                //open door
                doorAnim.SetTrigger("OpenDoor");
                //spawns enemies in area
                //areaToSpawn.SetActive(true);
            }
        }
    }
}
