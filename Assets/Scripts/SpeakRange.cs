using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakRange : MonoBehaviour
{
    [SerializeField] Character character;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            character.isPlayerInSpeakRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            character.isPlayerInSpeakRange = false;
        }
    }
}
