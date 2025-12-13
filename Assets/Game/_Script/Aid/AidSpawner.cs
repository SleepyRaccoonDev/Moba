using System.Collections;
using UnityEngine;

public class AidSpawner
{
    private const float NearestDistanceForSpawn = 5f;
    private const float SpawnHeight = 4f;

    private MonoBehaviour _mono;

    private Aid _aidPrefab;
    private float _spawnDistance;
    private float _spawnTime;

    private Coroutine _currentCoroutine;
    private YieldInstruction _waiter;

    private float _healthToRestore;

    private bool _isSpawnerActive;

    private AudioSource _audioSource;
    private AudioClip _audio;

    public AidSpawner(MonoBehaviour mono, Aid aidPrefab, AudioSource audioSource, float spawnDistance, float spawnTime, float healthToRestore, AudioClip audio)
    {
        _mono = mono;
        _aidPrefab = aidPrefab;
        _spawnDistance = spawnDistance;
        _spawnTime = spawnTime;
        _healthToRestore = healthToRestore;
        _audioSource = audioSource;
        _audio = audio;

        _waiter = new WaitForSeconds(_spawnTime);
    }

    public void ActivateSpawner()
    {
        _isSpawnerActive = !_isSpawnerActive;

        if (_isSpawnerActive)
            _currentCoroutine = _mono.StartCoroutine(SpawnProcces());
        else
        {
            if (_currentCoroutine != null)
                _mono.StopCoroutine(_currentCoroutine);
        }
    }

    private IEnumerator SpawnProcces()
    {
        while (true)
        {
            var randomDistance = Random.Range(NearestDistanceForSpawn, _spawnDistance);
            var random = Random.insideUnitCircle * randomDistance;
            Vector3 spawnPoint = _mono.transform.position + new Vector3(random.x, SpawnHeight, random.y);

            var aid = GameObject.Instantiate(_aidPrefab, spawnPoint, Quaternion.identity);
            aid.Initialize(_audioSource, _healthToRestore, _audio);

            yield return _waiter;
        }
    }
}