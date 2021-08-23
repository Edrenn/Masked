using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    private bool isReadyToBeDestroyed = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isReadyToBeDestroyed){
            Destroy(this.gameObject);
            FindObjectOfType<Player>().SetCanDoThings(true);
        }
    }

    public void SetReadyToDestroy(){
        isReadyToBeDestroyed = true;
    }
}
