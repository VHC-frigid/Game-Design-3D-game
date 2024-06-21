using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyType type;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case KeyType.Key1:
                    other.GetComponent<PlayerInventory>().hasKey1 = true;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
public enum KeyType
{
    Key1
}
