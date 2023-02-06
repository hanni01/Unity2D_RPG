using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_02 : MonoBehaviour
{
    public Transform player;
    public Transform teleportS;
    bool trigger_teleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger_teleport = true;
    }

    private void Update()
    {
        if (trigger_teleport)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                trigger_teleport = false;
                player.position = teleportS.position;
            }
        }
    }
}
