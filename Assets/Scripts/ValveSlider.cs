using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnGoalReached();
public delegate void OnGoalUnreached();

public class ValveSlider : MonoBehaviour
{
    [SerializeField] int goal;
    [Header("wrongColorBlock")]
    [SerializeField] ColorBlock wrongColorBlock;
    [Header("doneColorBlock")]
    [SerializeField] ColorBlock doneColorBlock;
    [SerializeField] Image currentSliderImg;

    public OnGoalReached onGoalReachedEvent;
    public OnGoalUnreached onGoalUnreachedEvent;
    private Slider currentSlider;
    private bool isGoalReached = false;

    private void Start() {
        currentSlider = GetComponent<Slider>();
        goal = Random.Range(0,11); 
        PlaceSliderOnAnyValueButGoal();
    }

    public void CheckCurrentValue(){
        if (currentSlider.value == goal){
            currentSlider.colors = doneColorBlock;
            currentSliderImg.color = doneColorBlock.normalColor;
            isGoalReached = true;
            onGoalReachedEvent.Invoke();
        }
        else{
            currentSlider.colors = wrongColorBlock;
            currentSliderImg.color = wrongColorBlock.normalColor;
            if (isGoalReached){
                onGoalUnreachedEvent.Invoke();
            }
            isGoalReached = false;
        }
    }

    public void SetSliderUninteractable(){
        currentSlider.interactable = false;
    }

    private void PlaceSliderOnAnyValueButGoal(){

        do
        {
            currentSlider.value = Random.Range(0,11);
        } while (currentSlider.value == goal);
    }
}
