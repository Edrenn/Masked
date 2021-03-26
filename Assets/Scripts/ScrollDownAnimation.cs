using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDownAnimation : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed);
        if (transform.position.y >= 39)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 78, 0);
        }
    }
}
