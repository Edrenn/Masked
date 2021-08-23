using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingMiniGameTrigger : MiniGameTrigger
{
    [SerializeField] private Animator alertFilterAnimator;

    protected override void ShowUI(bool value)
    {
        base.ShowUI(value);
        alertFilterAnimator.SetBool("isAlertOn",false);
    }

    new void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isPlayerInRange){
            ShowUI(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isPlayerInRange){
            ShowUI(false);
        }
    }
}
