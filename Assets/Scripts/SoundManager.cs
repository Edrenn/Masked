using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            OptionManager optionManager = FindObjectOfType<OptionManager>();
            optionManager.SoundVolumeUpdated += UpdateVolume;
        }
        else
        {
            UpdateVolume(PlayerPrefs.GetFloat(OptionManager.SOUNDVOLUME_KEY));
        }
    }

    private void UpdateVolume(float newValue)
    {
        audioSource.volume = newValue;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
