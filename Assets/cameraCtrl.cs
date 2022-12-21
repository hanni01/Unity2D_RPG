using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtrl : MonoBehaviour
{
    public GameObject model;
    private Transform playerTransform;
    public Vector3 cameraPosition;
    private float CameraZ = -10f;
    public Vector2 center;
    public Vector2 mapSize;

    private float height;
    private float width;
    private float cameraMoveSpeed = 4;

    void Start()
    {
        playerTransform = model.GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width/Screen.height;
    } 
    void FixedUpdate()
    {
        CameraMoveAndLimit();
    }

    void CameraMoveAndLimit()
    {
        //Time.deltaTime을 사용한 것은 프레임에 따른 시간 차이를 똑같이 맞추기 위해서
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, 2f*Time.deltaTime * cameraMoveSpeed);

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, CameraZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
