using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public delegate void MusicVolumeUpdated(float value);
public delegate void SoundVolumeUpdated(float value);
public class OptionManager : MonoBehaviour
{
    public const string MUSICVOLUME_KEY = "MusicVolume";
    public const string SOUNDVOLUME_KEY = "SoundVolume";
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider soundSlider;

    public MusicVolumeUpdated MusicVolumeUpdated;
    public SoundVolumeUpdated SoundVolumeUpdated;

    List<AudioSource> allAudioSources;

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>().ToList();
        volumeSlider.value = PlayerPrefs.GetFloat(MUSICVOLUME_KEY);
        soundSlider.value = PlayerPrefs.GetFloat(SOUNDVOLUME_KEY);
    }

    public void VolumeValueChanged()
    {
        MusicVolumeUpdated(volumeSlider.value);
    }
    public void SoundValueChanged()
    {
        SoundVolumeUpdated(soundSlider.value);
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat(MUSICVOLUME_KEY, volumeSlider.value);
        PlayerPrefs.SetFloat(SOUNDVOLUME_KEY, soundSlider.value);
    }

    public void QuitAndSave()
    {
        SaveOptions();
        FindObjectOfType<MainMenuHUDManager>().HideOptions();
    }
}
