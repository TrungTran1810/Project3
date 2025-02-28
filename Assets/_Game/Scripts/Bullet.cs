using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Gây sát thương nếu cần
            gameObject.SetActive(false); // Không hủy mà chỉ tắt đi
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime(3f)); // Tắt sau 3s nếu không trúng mục tiêu
    }

    private IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

