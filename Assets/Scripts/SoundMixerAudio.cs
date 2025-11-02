using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMainVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }


}
