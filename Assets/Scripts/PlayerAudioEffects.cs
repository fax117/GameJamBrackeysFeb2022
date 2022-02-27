using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    [Header("Sound Effects clips")]
    [SerializeField] protected AudioClip shootEffect;
    [SerializeField] protected AudioClip dyingEffect;
    [SerializeField] protected AudioClip hurtEffect;

    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ShootingEffect()
    {
        _audioSource.loop = true;
        _audioSource.pitch = 3f;
        _audioSource.clip = shootEffect;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
    }

    public void DyingEffect()
    {
        _audioSource.PlayOneShot(dyingEffect, 0.9f);
    }

    public void HurtEffect()
    {
        _audioSource.PlayOneShot(hurtEffect, 0.9f);
    }
    
    public void PauseEffect()
    {
        _audioSource.clip = null;
        _audioSource.pitch = 1f;
        _audioSource.loop = false;
        _audioSource.Pause();
        
    }
}
