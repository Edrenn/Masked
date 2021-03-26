using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{    
    [SerializeField] private int incrementValue;
    [SerializeField] private List<ProgressCheckpoint> checkpoints;
    private ProgressCheckpoint currentCheckpoint;
    private Slider slider;
    private bool isActivated = false;

    private void Start() {
       slider = GetComponent<Slider>();
       foreach (var checkpoint in checkpoints)
       {
           checkpoint.OnCheckpointReady += CheckpointPassed;
       }
       currentCheckpoint = checkpoints.First();
    }

    private void Update() {
        if (isActivated)
        {
            slider.value += incrementValue * Time.deltaTime;
            if (currentCheckpoint != null && slider.value >= currentCheckpoint.stopValue){
                Desactivate();
                currentCheckpoint.EnableAlert(true);
                Debug.Log("Stopped due to checkpoint not ready !");
                // Show UI warning that process is blocked
            }
        }
    }

    public void Activate(){
        isActivated = true;
        Debug.Log("Process activated !");
    } 

    public void Desactivate(){
        isActivated = false;
        Debug.Log("Process DESactivated !");
    } 

    private void SetNextCheckpoint(){
        currentCheckpoint.EnableAlert(false);
        Debug.Log("Current checkpoint id : " + checkpoints.IndexOf(currentCheckpoint));
        if (currentCheckpoint != checkpoints.Last()){
            currentCheckpoint = checkpoints[checkpoints.IndexOf(currentCheckpoint) + 1];
        }
        else{
            currentCheckpoint = null;
        }
    }

    private void CheckpointPassed(){
        SetNextCheckpoint();
        Activate();
    }
}
