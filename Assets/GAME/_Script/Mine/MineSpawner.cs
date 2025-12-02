using UnityEngine;

public class MineSpawner 
{
    private Transform _spawnField;
    private GameObject _minePrefab;

    public MineSpawner(Transform spawnField, GameObject minePrefab)
    {
        _spawnField = spawnField;
        _minePrefab = minePrefab;
    }

    public void Spawn()
    {
        foreach (Transform point in _spawnField)
        {
            GameObject.Instantiate(_minePrefab, point.position, Quaternion.identity);
        }
    }
}