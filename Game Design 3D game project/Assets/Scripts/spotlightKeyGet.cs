using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spotlightKeyGet : MonoBehaviour
{
    [SerializeField] public GameObject spotlight;
    [SerializeField] public GameObject key;
    [SerializeField] public float fadeoutSeconds;
    public bool keyGot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (key.IsDestroyed())
        {
            spotlight.transform.position -= new Vector3(0, fadeoutSeconds * Time.deltaTime, 0);
        }
    }
}
