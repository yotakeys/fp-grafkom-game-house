using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            #region smoke effect
            GameObject smokeEffect = ObjectPooling.instance.GetPooledObject("SmokeEffect");
            smokeEffect.transform.position = transform.position;
            smokeEffect.SetActive(true);
            smokeEffect.GetComponent<Animator>().Play("SmokeEffect", -1, 0);
            #endregion

            #region Coin
            int r = Random.Range(0, 4);
            if (r == 2)
            {
                GameObject coin = ObjectPooling.instance.GetPooledObject("Coin");
                coin.transform.position = transform.position;
                coin.SetActive(true);
            }
            #endregion

            MenuManager.instance.IncreaseScore();
            gameObject.SetActive(false);
        }
    }
}
