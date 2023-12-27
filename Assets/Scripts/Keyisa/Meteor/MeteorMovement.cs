using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorMovement : MonoBehaviour
{

    public float speed = 10f;

    public List<Sprite> imageList = new List<Sprite>();

    private void Start()
    {
        speed = Random.Range(5, 12);

        Sprite randomImage = GetRandomImage();
        transform.GetComponent<SpriteRenderer>().sprite = randomImage;

    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;

        transform.Rotate(0, 0, 0.5f);
        
        if(pos.x <= Data.minX)
        {
            Destroy(gameObject);
            Data.scores += 10;
        }

    }

    Sprite GetRandomImage()
    {
        if (imageList.Count > 0)
        {
            int randomIndex = Random.Range(0, imageList.Count);
            return imageList[randomIndex];
        }
        else
        {
            Debug.LogWarning("No images in the collection!");
            return null;
        }
    }
}
