using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class HiddenWeapon : MonoBehaviour
{
    [SerializeField] private Animator unlockAnimator;
    private bool isPlayerInRange = false;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerInRange)
        {
            unlockAnimator.SetTrigger("UnlockMask");
            FindObjectOfType<Player>().SetCanDoThings(false);
            FindObjectOfType<Player>().SetCanTakeDamage(false);
            FindObjectOfType<FireBehaviour>().UnlockMask();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
            isPlayerInRange = false;
    }
}
