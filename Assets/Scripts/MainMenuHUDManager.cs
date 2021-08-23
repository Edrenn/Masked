using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHUDManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuInterface;
    [SerializeField] GameObject OptionsInterface;
    [SerializeField] Animator MenuAnimator;

    public void ShowOptions()
    {
        MenuAnimator.SetTrigger("Options");
    }

    public void HideOptions()
    {
        MenuAnimator.SetTrigger("MainMenu");
    }
}
