using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanel : MonoBehaviour
{ 
    public Text lifes;
    public void Update()
    {
        lifes.text = "LIFES : " + Data.lifes.ToString();
    }
}
