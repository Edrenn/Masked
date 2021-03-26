using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimTriggerZone : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

    bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (played == false && collision.tag == "Player")
        {
            playableDirector.Play();
            played = true;
        }
    }
}
