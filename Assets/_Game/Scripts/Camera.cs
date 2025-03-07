using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform target; // Nhân vật cần theo dõi
    public float speed = 1f;
    public Vector3 offSet = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Tính toán vị trí mong muốn của camera
        //Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

        // Di chuyển camera mượt mà
        transform.position = Vector3.Lerp(this.transform.position, target.position + offSet , speed );

        // Camera luôn nhìn về nhân vật
        //transform.L(target.position + Vector3.up * 1.5f);
    }
}


