using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraCtrl : MonoBehaviour
{
    public Vector3 cameraP = new Vector3(0, 0, -10);
    public Transform player;
    public float cameraMoveSpeed;
    private float height;
    private float width;
    public Vector2 center;
    public Vector2 mapSize;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    void FixedUpdate()
    {
        CameraArea();
    }

    void CameraArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            player.position + cameraP, Time.deltaTime * cameraMoveSpeed);
        float limitX = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -limitX + center.x, limitX + center.x);

        float limitY = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -limitY + center.y, limitY + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
