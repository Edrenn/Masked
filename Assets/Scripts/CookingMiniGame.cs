using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookingHeat
{
    low,
    medium,
    High
}

[System.Serializable]
public class CookingStep
{
    public float Duration;
    [SerializeField] private CookingHeat requiredHeat;
    public CookingHeat RequiredHeat { get => requiredHeat; }
}

public class CookingMiniGame : MonoBehaviour
{
    [SerializeField] private List<CookingStep> stepsList;
    private Queue<CookingStep> stepsQueue;
    [SerializeField] private GameObject alertUI;
    [SerializeField] private Animator alertFilterAnimator;

    // Heat limit
    [SerializeField] private int lowHeatMaximumValue;
    [SerializeField] private int mediumHeatMaximumValue;
    [SerializeField] private int HighHeatMaximumValue;

    private CookingHeat currentHeat;
    private CookingStep currentStep;
    private bool isLaunched = false;

    private void Start() {
        stepsQueue = new Queue<CookingStep>();
        foreach (var step in stepsList)
        {
            stepsQueue.Enqueue(step);
        }
    }

    void Update()
    {
        if (isLaunched){
            // is current step finished
            if (currentStep.Duration <= 0){
                NextStep();
            }
            else if (currentStep.RequiredHeat == currentHeat)
            {
                currentStep.Duration -= Time.deltaTime;
                alertUI.SetActive(false);
                alertFilterAnimator.SetBool("isAlertOn",false);
            }
            else{

                if (!alertUI.activeInHierarchy)
                    alertUI.SetActive(true);
                alertFilterAnimator.SetBool("isAlertOn",true);
            }
        }
    }

    public void OnValueChange(float value){
        if (value <= lowHeatMaximumValue)
            currentHeat = CookingHeat.low;
        else if (value <= mediumHeatMaximumValue)
            currentHeat = CookingHeat.medium;
        else
            currentHeat = CookingHeat.High;
    }

    public void Launch(){
        isLaunched = true;
        NextStep();
    }

    private void NextStep(){
        if (stepsQueue != null && stepsQueue.Count > 0)
            currentStep = stepsQueue.Dequeue();
        else if (stepsQueue != null && stepsQueue.Count == 0){
            // TODO finish
                alertUI.SetActive(false);
            isLaunched = false;
            
            Debug.Log("FINISHED");
        }
        else
        {
            // Error
        }

    }
}
