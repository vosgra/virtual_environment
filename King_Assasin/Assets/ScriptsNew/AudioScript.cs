using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider, musicSlider, sfxSlider, dialogueSlider;

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("AmbientVolume", volume);
        PlayerPrefs.SetFloat("AmbientVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetDialogueVolume(float volume)
    {
        audioMixer.SetFloat("DialogueVolume", volume);
        PlayerPrefs.SetFloat("DialogueVolume", volume);
    }

    private void LoadVolumeSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("AmbientVolume", 0);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);
        dialogueSlider.value = PlayerPrefs.GetFloat("DialogueVolume", 0);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetDialogueVolume(dialogueSlider.value);
    }
}
