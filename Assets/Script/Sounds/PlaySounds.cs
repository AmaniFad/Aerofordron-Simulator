using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance foosteps;
    const string VCAPath = "vca:/";
    const string generalVCAPath = "General";
    const string musicVCAPath = "Music";
    const string SFXVCAPath = "SFX";


    VCA generalVCA;
    VCA sfxVCA;
    VCA musicVCA;


    public void Start()
    {
        generalVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + generalVCAPath);
        sfxVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + musicVCAPath);
        musicVCA = FMODUnity.RuntimeManager.GetVCA(VCAPath + SFXVCAPath);
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


}
