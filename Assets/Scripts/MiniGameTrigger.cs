using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject miniGameUI;
    private bool isPlayerInRange;
    
    private void ShowUI(bool value){
        FindObjectOfType<Player>().SetCanDoThings(!value);
        miniGameUI.SetActive(value);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isPlayerInRange){
            ShowUI(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isPlayerInRange){
            ShowUI(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            isPlayerInRange = false;
        }
    }
}
