using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLight : MonoBehaviour
{

    [SerializeField] public Light GunLightObj;
    [SerializeField] public float gunFlashTime;
    [SerializeField] public float gunLightIntensity;
    private float gunLightLerp;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GunLightObj.intensity = gunLightIntensity;
        }


        GunLightObj.intensity = Mathf.Clamp(GunLightObj.intensity - gunFlashTime * Time.deltaTime * gunLightIntensity, 0, gunLightIntensity);
    }
}
