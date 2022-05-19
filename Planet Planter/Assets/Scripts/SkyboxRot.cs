using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRot : MonoBehaviour
{

    public float rotationSpeed = 1.2f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}