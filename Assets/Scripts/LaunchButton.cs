using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    public static bool isLaunched = false;
    [SerializeField] Sprite LaunchedSprite;
    bool isPlayerInRange;
    
    private void Activate(){
        GetComponent<SpriteRenderer>().sprite = LaunchedSprite;
        FindObjectOfType<ProgressSlider>().Activate();
        isLaunched = true;
        FindObjectOfType<CookingMiniGame>().Launch();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isPlayerInRange){
            Activate();
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
