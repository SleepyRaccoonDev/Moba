using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour, IDisposable
{
    private const float SoundOnValue = 0;
    private const float SoundOffValue = -80;

    private const string SoundValueKey = "SoundVolume";
    private const string MusicValueKey = "MusicVolume";

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] AudioMixer _audioMixer;

    private bool _isSoundOff = true;
    private bool _isMusicOff = true;

    private void Awake()
    {
        _soundButton.onClick.AddListener(SwitchSound);
        _musicButton.onClick.AddListener(SwitchMusic);
    }

    private void SwitchSound()
    {
        _isSoundOff = !_isSoundOff;

        float value = _isSoundOff ? SoundOnValue : SoundOffValue;

        _audioMixer.SetFloat(SoundValueKey, value);
    }

    private void SwitchMusic()
    {
        _isMusicOff = !_isMusicOff;

        float value = _isMusicOff ? SoundOnValue : SoundOffValue;

        _audioMixer.SetFloat(MusicValueKey, value);
    }

    public void Dispose()
    {
        _soundButton.onClick.RemoveListener(SwitchSound);
        _musicButton.onClick.RemoveListener(SwitchMusic);
    }
}