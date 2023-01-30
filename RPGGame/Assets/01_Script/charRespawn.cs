using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class charRespawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] respawnP = new GameObject[4];
    public bool isFloorEnd = false;
    GameObject nearRespawn;
    float dist;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "floorEnd")
        {
            isFloorEnd = true;
            transform.position = CalculateDist().transform.position;
        }
    }

    GameObject CalculateDist()
    {
        nearRespawn = respawnP[0];
        dist = Vector2.Distance(Player.transform.position, nearRespawn.transform.position);
        for(int i = 1;i < respawnP.Length;i++)
        {
            if(dist > Vector2.Distance(Player.transform.position, respawnP[i].transform.position))
            {
                nearRespawn = respawnP[i];
            }
        }
        return nearRespawn;
    }
}
