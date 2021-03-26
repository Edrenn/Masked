using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiStop : MonoBehaviour
{
    bool isPlayerInRange = false;
    bool hasSpoken = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Use") && hasSpoken == false)
        {
            LaunchEndOfLevel();
        }
        else if (hasSpoken == true && Input.GetButtonDown("Use"))
        {
            FindObjectOfType<SceneController>().LoadLevel(2);
        }
    }

    void LaunchEndOfLevel()
    {
        DialogBox dialogBox = FindObjectOfType<DialogBox>();
        dialogBox.AddNewLine(new Dialog("Moi", "Le taxi pour le Quartier de l'Océan devrait arriver d'une minute à l'autre."));

        hasSpoken = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
    }
}
