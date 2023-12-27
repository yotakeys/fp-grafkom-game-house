 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteor;
    private int nextUpdate = 1;

    void Update()
    {

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            var position = new Vector3(Data.maxX, Random.Range(Data.minY, Data.maxY), 0);
            Instantiate(meteor, position, Quaternion.identity, transform);
        }

    }
}
