using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected AudioClip[] _clips;
    private AudioSource _audioSource;
    private GameObject _pauseMenu;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _pauseMenu = GameObject.Find("PauseMenuCanvas").transform.Find("PauseMenu").gameObject;
        _audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pauseMenu.activeInHierarchy)
        {
            _audioSource.Pause();
        }
        
        if (!_audioSource.isPlaying && !_pauseMenu.activeInHierarchy)
        {
            _audioSource.clip = GetRandomClip();
            _audioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Length)];
    } 

}
