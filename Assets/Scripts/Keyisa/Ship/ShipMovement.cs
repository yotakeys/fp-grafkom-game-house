using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    public float speed = 10f;

    private float spriteWidth;
    private float spriteHeight;

    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    void Update()
    {

        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }


        float minX = Data.minX + spriteWidth / 2f;
        float maxX = Data.maxX - spriteWidth / 2f;
        float minY = Data.minY + spriteWidth / 2f;
        float maxY = Data.maxY - spriteWidth / 2f;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}