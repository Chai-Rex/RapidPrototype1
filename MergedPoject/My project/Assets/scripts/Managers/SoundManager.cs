using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";


    [SerializeField] private AudioClipRefSO audioClipRefsSO;

    private float volume = 0.5f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.5f);
    }

    //========================================================================================
    public void SoundInvaderBallBounce(Vector3 position) {
        PlaySound(audioClipRefsSO.invaderBallBounce, position);
    }
    public void SoundPlayerBallBounce(Vector3 position) {
        PlaySound(audioClipRefsSO.playerBallBounce, position);
    }
    public void SoundPlanetBallBounce(Vector3 position) {
        PlaySound(audioClipRefsSO.planetBallBounce, position);
    }
    public void SoundProjectileBounce(Vector3 position) {
        PlaySound(audioClipRefsSO.projectileBounce, position, 0.25f);
    }
    //========================================================================================
    public void SoundProjectileDamageHP(Vector3 position) {
        PlaySound(audioClipRefsSO.projectileDamageHP, position);
    }
    public void SoundInvaderDamageHP(Vector3 position) {
        PlaySound(audioClipRefsSO.invaderDamageHP, position);
    }
    public void SoundGainHP(Vector3 position) {
        PlaySound(audioClipRefsSO.gainHP, position);
    }
    //========================================================================================
    public void SoundProjectileExplosion(Vector3 position) {
        PlaySound(audioClipRefsSO.projectileExplosion, position);
    }
    public void SoundInvaderExplosion(Vector3 position) {
        PlaySound(audioClipRefsSO.invaderExplosion, position);
    }
    //========================================================================================
    public void SoundInvaderShoot(Vector3 position) {
        PlaySound(audioClipRefsSO.invaderShoot, position, 0.5f);
    }
    public void SoundPlayerShoot(Vector3 position) {
        PlaySound(audioClipRefsSO.playerShoot, position);
    }
    //========================================================================================
    public void SoundGameOver(Vector3 position) {
        PlaySound(audioClipRefsSO.gameOver, position);
    }
    //========================================================================================

    // play random audio clip from array
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    // play specific audio clip from array
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void ChangeVolume(float newVolume) {
        volume = newVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
