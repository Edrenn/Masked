using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFall : MonoBehaviour
{
    [SerializeField] private bool isPlayerInZone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInZone = false;
        }
    }

    public void CrushPlayer()
    {
        if (isPlayerInZone)
        {
            FindObjectOfType<Player>().TakeDamage();
        }
    }
}
