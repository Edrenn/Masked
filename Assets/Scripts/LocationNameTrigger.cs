using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationNameTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            animator.SetBool("ShowLocationName", true);
        }
    }
}
