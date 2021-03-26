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
    }

    private void OnDisable() {
        if (isReady == false && LaunchButton.isLaunched)
            stopAlert.SetActive(true);
    }
}
