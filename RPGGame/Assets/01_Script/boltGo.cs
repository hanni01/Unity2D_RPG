using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltGo : MonoBehaviour
{
    public float speed = 50f;
    private Vector3 direction;

    private void Start() {
        if(GameObject.Find("Wizard").GetComponent<Transform>().localScale.x == 0.5f)
        {
            direction = Vector3.left;
        }
        else if(GameObject.Find("Wizard").GetComponent<Transform>().localScale.x == -0.5f)
        {
            direction = Vector3.right;
        }
    }
    void Update()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }
}
