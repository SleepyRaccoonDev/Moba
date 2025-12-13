using UnityEngine;

public class MineSpawner 
{
    private Transform _spawnField;
    private Mine _minePrefab;

    private AudioSource _audioSource;
    private AudioClip _audio;

    public MineSpawner(Transform spawnField, Mine minePrefab, AudioSource audioSource, AudioClip audio)
    {
        _spawnField = spawnField;
        _minePrefab = minePrefab;

        _audioSource = audioSource;
        _audio = audio;
    }

    public void Spawn()
    {
        foreach (Transform point in _spawnField)
        {
            Mine mine = GameObject.Instantiate(_minePrefab, point.position, Quaternion.identity);
            mine.Initialize(_audioSource, _audio);
        }
    }
}