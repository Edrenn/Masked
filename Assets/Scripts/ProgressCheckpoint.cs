using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCheckpointReady();

public class ProgressCheckpoint : MonoBehaviour, ICheckpoint
{
    [SerializeField] public int stopValue;
    [SerializeField] private GameObject stopAlert;
    public OnCheckpointReady OnCheckpointReady;
    private bool isReady = false;
    private bool isFocused = false;

    // TODO DELETE
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K) && isFocused)
        {
            SetCheckpointReady();
        }    
    }

    public bool IsCheckpointReady()
    {
        return isReady;
    }

    public void SetCheckpointReady()
    {
        isReady = true;
        OnCheckpointReady.Invoke();
    }

    public void EnableAlert(bool value){
        stopAlert.SetActive(value);
    }

    public void Hide(){
        GetComponent<Animator>().SetTrigger("Trigger");
    }

    private void OnEnable() {
        stopAlert.SetActive(false);
        isFocused = true;
    }

    private void OnDisable() {
        if (isReady == false && LaunchButton.isLaunched)
            stopAlert.SetActive(true);

        isFocused =false;
    }
}
