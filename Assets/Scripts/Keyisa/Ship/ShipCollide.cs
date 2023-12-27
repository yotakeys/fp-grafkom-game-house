using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollide : MonoBehaviour
{
    public GameObject LosePanel;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            Destroy(collision.gameObject);
            Data.lifes -= 1;

            if(Data.lifes <=0 )
            {
                LosePanel.SetActive(true);
            }
        }
    }
}
