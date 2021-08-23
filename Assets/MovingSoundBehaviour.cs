using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoundBehaviour : MonoBehaviour
{
    public bool isGroundWet = false;
    private AudioSource audioSource;

    private bool hardGround1Played = false;
    [SerializeField] private AudioClip hardGround1;
    [SerializeField] private AudioClip hardGround2;
    
    private bool wetGround1Played = false;
    [SerializeField] private AudioClip wetGround1;
    [SerializeField] private AudioClip wetGround2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkSound(){
        if (isGroundWet)
            PlayWetGroundSound();
        else
            PlayHardGroundSound();
    }

    private void PlayHardGroundSound(){
        if (hardGround1Played){
            audioSource.PlayOneShot(hardGround1);
            hardGround1Played = false;
        }else{
            audioSource.PlayOneShot(hardGround2);
            hardGround1Played = true;
        }
    }

    private void PlayWetGroundSound(){
        if (wetGround1Played){
            audioSource.PlayOneShot(wetGround1);
            wetGround1Played = false;
        }else{
            audioSource.PlayOneShot(wetGround2);
            wetGround1Played = true;
        }
    }
}
