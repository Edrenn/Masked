using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventEmitter : MonoBehaviour
{
    public void ShakeCamera(){
        Camera.main.GetComponent<Animator>().SetTrigger("SmallShake");
    }
}
