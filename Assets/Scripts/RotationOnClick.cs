using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnClick : MonoBehaviour
{
    public bool isMouseFocused = false;
    public bool isRotationEnabled = true;
    protected Transform currentTransform;

    protected SpriteRenderer currentSpriteRenderer;
    
    protected void Start() {
        currentTransform = GetComponent<Transform>();
        currentSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMouseFocused && isRotationEnabled){
            Rotate();
        }
    }

    public void SetRotationEnabled(bool value){
        isRotationEnabled = value;
    }

    protected virtual void Rotate(){
        currentTransform.Rotate(new Vector3(0,0,-90),Space.Self);
    }

    private void OnMouseEnter() {
        isMouseFocused = true;
        SetHighlightedColor(true);
    }

    private void OnMouseExit() {
        isMouseFocused = false;
        SetHighlightedColor(false);
    }

    private void SetHighlightedColor(bool value){
        if (value)
            currentSpriteRenderer.color = Color.grey;
        else
            currentSpriteRenderer.color = Color.white;
    }
}
