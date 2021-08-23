using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNode : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private bool canRotate = false;
    [SerializeField] private RotationNode nextNode;

    private OnGoalReached OnGoalReached;
    private float minRotationGoal;
    private float maxRotationGoal;
    private Transform currentTransform;
    private bool isGoalReached = false;

    void Start() {
        currentTransform = GetComponent<Transform>();

        float randomRotation = UnityEngine.Random.Range(-90,266);
        targetTransform.rotation = Quaternion.Euler(0,0,randomRotation);
        minRotationGoal = targetTransform.rotation.eulerAngles.z -10;
        maxRotationGoal = targetTransform.rotation.eulerAngles.z +10;
    }
    

    void Update()
    {
        if (canRotate)
            currentTransform.Rotate(new Vector3(0,0, rotationSpeed * Time.deltaTime),Space.Self);

        if (Input.GetMouseButtonDown(0) && !isGoalReached){
            CheckValueReached();
        }
    }

    public void SetOnGoalReached(OnGoalReached value){
        OnGoalReached = value;
    }

    public bool GetIsGoalReached(){
        return isGoalReached;
    }

    public void SetCanRotate(bool value){
        canRotate = value;
    }

    private void CheckValueReached()
    {
        if (currentTransform.rotation.eulerAngles.z > minRotationGoal && currentTransform.rotation.eulerAngles.z < maxRotationGoal)
        {
            isGoalReached = true;
            SetCanRotate(false);
            if (nextNode != null)
                nextNode.SetCanRotate(true);
            else
                OnGoalReached.Invoke();
        }
        else
        {
            currentTransform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
