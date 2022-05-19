using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRot : MonoBehaviour
{
    public float rotateSpeed = 1;
    public Transform Sphere;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sphere.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
    }
}
