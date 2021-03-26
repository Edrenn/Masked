using UnityEngine;
using System.Collections;

public delegate void OnDetectionIn();
public delegate void OnDetectionOut();

public class DetectionRange : MonoBehaviour
{
    public OnDetectionIn OnDetectionIn;
    public OnDetectionIn OnDetectionOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnDetectionIn.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnDetectionOut.Invoke();
        }
    }
}
