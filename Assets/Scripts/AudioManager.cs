using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip bg;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip win;
    public AudioClip point;
    public AudioClip jump;

    private void Start()
    {
        musicSource.clip = bg;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
