using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTest : MonoBehaviour
{
    public GameObject model;
    Transform model_t;

    void Start() {
        model_t =  model.transform;    
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, model_t.position, 2f*Time.deltaTime);
        transform.Translate(0, 0, -10);
    }
}