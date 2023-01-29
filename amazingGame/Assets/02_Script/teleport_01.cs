using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_01 : MonoBehaviour
{
    public Transform player;
    public Transform teleportD;
    public Transform teleportS;
    bool trigger_teleport = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter TeleportS");
        trigger_teleport = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Detect Trigger Exit");
        trigger_teleport = false;
    }

    private void Update()
    {
        if(trigger_teleport) 
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if(this.gameObject.name == "teleportS")
                {
                    player.position = teleportD.position;
                }
                else if(this.gameObject.name == "teleportD")
                {
                    player.position = teleportS.position;
                }
            }
        }
    }
}
