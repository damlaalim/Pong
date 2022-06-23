using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip score;
    [SerializeField] private AudioClip gameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Play(AudioType.Theme);
    }

    public void Play(AudioType type)
    {
        switch (type)
        {
            case AudioType.Theme:
                musicSource.clip = theme;
                musicSource.Play();
                break;
            case AudioType.Hit:
                effectsSource.clip = hit;
                effectsSource.Play();
                break;
            case AudioType.Click:
                effectsSource.clip = click;
                effectsSource.Play();
                break;
            case AudioType.Score:
                effectsSource.clip = score;
                effectsSource.Play();
                break;
            case AudioType.GameOver:
                effectsSource.clip = gameOver;
                effectsSource.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

public enum AudioType
{
    Theme,
    Hit,
    Click,
    Score,
    GameOver
}
