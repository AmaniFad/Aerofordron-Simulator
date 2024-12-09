using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance foosteps;
    const string VCAPath = "vca:/";
    const string generalVCAPath = "General";
    const string musicVCAPath = "Music";
    const string SFXVCAPath = "SFX";
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SfxSlider;

    VCA generalVCA;
    VCA sfxVCA;
    VCA musicVCA;


    public void Start()
    {

        generalVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + generalVCAPath);
        sfxVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + musicVCAPath);
        musicVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + SFXVCAPath);
        if (MasterSlider && MusicSlider && SfxSlider)
        {
            SetSliders();
        }
    }
    private void PlayFootstep()
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }
    public void CallOneShot(string eventRoute)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRoute);
    }

    public void ChangeVolumeMusic(float volume)
    {
        musicVCA.setVolume(volume);
    }

    public void ChangeVolumeSFX(float volume)
    {
        sfxVCA.setVolume(volume);
    }
    public void ChangeVolumeMaster(float volume)
    {
        generalVCA.setVolume(volume);
    }

    public void SetSliders()
    {
        generalVCA.getVolume(out float volume);
        musicVCA.getVolume(out float musicVolume);
        sfxVCA.getVolume(out float sfxVolume);
        MasterSlider.value = volume;
        MusicSlider.value = musicVolume;
        SfxSlider.value = sfxVolume;

        
    }


}
