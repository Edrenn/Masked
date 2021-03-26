using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHUDManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuInterface;
    [SerializeField] GameObject OptionsInterface;

    public void ShowOptions()
    {
        OptionsInterface.SetActive(true);
        MainMenuInterface.SetActive(false);
    }

    public void HideOptions()
    {
        OptionsInterface.SetActive(false);
        MainMenuInterface.SetActive(true);
    }
}
