using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform target; // Nhân vật cần theo dõi
    public float distance = 5f;  // Khoảng cách camera với nhân vật
    public float height = 2f;    // Độ cao của camera
    public float smoothSpeed = 5f; // Độ mượt khi di chuyển

    void LateUpdate()
    {
        if (target == null) return;

        // Tính toán vị trí mong muốn của camera
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

        // Di chuyển camera mượt mà
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Camera luôn nhìn về nhân vật
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}


