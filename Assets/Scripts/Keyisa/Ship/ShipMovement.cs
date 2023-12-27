using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    public float speed = 10f;
    public GameObject rocket;
    public GameObject rocketParent;

    public float cooldownTime = 1f;

    private float nextFireTime = 0f;

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
        if (Input.GetKey("space") && Time.time > nextFireTime)
        {
            Instantiate(rocket, new Vector3(transform.position.x + spriteWidth, transform.position.y), Quaternion.Euler(0f, 0f, -90f), rocketParent.transform);
            nextFireTime = Time.time + cooldownTime;
        }


        float minX = Data.minX + spriteWidth / 2f;
        float maxX = Data.maxX - spriteHeight / 2f;
        float minY = Data.minY + spriteWidth / 2f;
        float maxY = Data.maxY - spriteHeight / 2f;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}