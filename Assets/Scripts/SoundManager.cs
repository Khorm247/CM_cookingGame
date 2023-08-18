using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipsRefSO audioClipsRefSO;

    private float defaultVolume = 0.5f;
    private float playerFootstepNoiseLevel = 2f;
    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, defaultVolume);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedUpAnything += Player_OnPickedUpAnything;
        TrashCounter.OnAnyItemTrashed += TrashCounter_OnAnyItemTrashed;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipsRefSO.objectDrop, baseCounter.transform.position);
    }

    private void TrashCounter_OnAnyItemTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trash = (TrashCounter)sender;
        PlaySound(audioClipsRefSO.trash, trash.transform.position);
    }

    private void Player_OnPickedUpAnything(object sender, System.EventArgs e)
    {        
        PlaySound(audioClipsRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipsRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefSO.deliverySuccess, Camera.main.transform.position, volume);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipsRefSO.deliveryFail, Camera.main.transform.position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position)
    {
        PlaySound(audioClipsRefSO.footstep[Random.Range(0, audioClipsRefSO.footstep.Length)], position, playerFootstepNoiseLevel);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
            volume = 0f;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
