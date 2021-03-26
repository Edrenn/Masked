using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnRotate();

public class RotationBlock : RotationOnClick
{
    [SerializeField] private List<float> targetZRotation;
    public bool isTargetReached = false;

    public OnRotate OnRotateEvent;
    private float currentZRotation;

    new private void Start() {
        base.Start();
        CheckRotation();
    }

    protected override void Rotate()
    {
        base.Rotate();
        CheckRotation();
        OnRotateEvent.Invoke();
    }

    private void CheckRotation(){
        // Use eulerAngles to get values in degrees (0,90,180,270)
        currentZRotation = currentTransform.eulerAngles.z;
        
        if (targetZRotation.Contains(currentZRotation))
            isTargetReached = true;
        else
            isTargetReached = false;
    }

    public void SetGreenColor(){
        base.currentSpriteRenderer.color = Color.green;
    }
}
