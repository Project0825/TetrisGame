using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private Ctrl ctrl;

    public AudioClip Cursor;
    public AudioClip Drop;
    public AudioClip Move;
    public AudioClip LineClear;

    private AudioSource audioSource;

    private bool isMute = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ctrl = GetComponent<Ctrl>();
    }
    public void PlayCursor()
    {
        PlayAudio(Cursor);
    }
    public void PlayDrop()
    {
        PlayAudio(Drop);
    }
    public void PlayMove()
    {
        PlayAudio(Move);
    }
    public void PlayAudioClear()
    {
        PlayAudio(LineClear);
    }
    private void PlayAudio(AudioClip clip)
    {
        if (isMute) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        if(isMute == false)
        {
            PlayCursor();
        }
    }
}
