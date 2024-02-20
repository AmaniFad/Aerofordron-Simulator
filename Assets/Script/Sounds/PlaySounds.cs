using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    private StudioBankLoader bankLoader;
    private FMOD.Studio.Bus musicBank;
    private FMOD.Studio.Bus sfxBank;
    private FMOD.Studio.Bus masterBank;
    private FMOD.Studio.EventInstance foosteps;

    private void Start()
    {
        masterBank = FMODUnity.RuntimeManager.GetBus("bus:/");
        musicBank = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBank = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

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
        musicBank.setVolume(volume);
    }

    public void ChangeVolumeSFX(float volume)
    {
        sfxBank.setVolume(volume);
    }
    public void ChangeVolumeMaster(float volume)
    {
        masterBank.setVolume(volume);
    }


}
