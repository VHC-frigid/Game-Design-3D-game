using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{

    [SerializeField] float delay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        } else
        {
            Destroy(gameObject);
            Debug.Log("destroyed");
        }

    }
}
