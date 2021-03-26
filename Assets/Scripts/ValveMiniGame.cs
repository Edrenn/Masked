using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class ValveMiniGame : ProgressCheckpoint
{
    List<ValveSlider> sliders;

    [SerializeField] private int sliderCounter;

    private void Start() {
        sliders = GetComponentsInChildren<ValveSlider>().ToList();
        sliderCounter = sliders.Count;

        foreach (var slider in sliders)
        {
            slider.onGoalReachedEvent += RemoveToCounter;
            slider.onGoalUnreachedEvent += AddToCounter;
        }
    }

    private void AddToCounter(){
        sliderCounter++;
    }

    private void RemoveToCounter(){
        sliderCounter--;
        
        if (sliderCounter == 0)
        {
            SetCheckpointReady();
            foreach (var slider in sliders)
            {
                slider.SetSliderUninteractable();
            }
        }
    }
}
