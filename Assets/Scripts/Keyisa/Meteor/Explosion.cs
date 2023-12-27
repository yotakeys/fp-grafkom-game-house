using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float destroyDelay = 0.5f;
    void Start()
    {
        // Panggil fungsi DestroyObject setelah delay
        Invoke("DestroyObject", destroyDelay);
    }

    void DestroyObject()
    {
        // Hancurkan objek ini setelah delay
        Destroy(gameObject);
    }
}
