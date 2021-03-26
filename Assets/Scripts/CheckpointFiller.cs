using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointFiller : MonoBehaviour
{
    [SerializeField] private float countdownSpeed = 0.016f;
    public bool isPaused = false;
    private Image filler;
    void Start()
    {
        filler = GetComponent<Image>();
    }

    void Update()
    {
        if (isPaused == false){
            filler.fillAmount -= countdownSpeed * Time.deltaTime;
            if (filler.fillAmount == 0){
                // TODO : Show game over and ask for restart level
                Debug.Log("KABOOM GAME OVER END OF THE LEVEL !");
            }
        }
    }

    public void SetPauseTimer(bool value){
        isPaused = value;
    }
}
