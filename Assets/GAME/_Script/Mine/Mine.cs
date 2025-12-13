using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject[] _effects;

    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;

    [SerializeField] private float _timeToExplose;

    private Collider[] _colliders;

    private Coroutine _currentProcess;

    private AudioSource _audioSource;
    private AudioClip _audio;

    private void Update()
    {
        if (_currentProcess == null && TargetDetector(out List<IDamageable> targets))
        {
            _currentProcess = StartCoroutine(ExplosiveProcess());
        }
    }

    public void Initialize(AudioSource audioSource, AudioClip audio)
    {
        _audioSource = audioSource;
        _audio = audio;
    }

    private IEnumerator ExplosiveProcess()
    {
        float timer = 0;

        while (timer < _timeToExplose)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (TargetDetector(out List<IDamageable> targets))
        {
            foreach (var target in targets)
                target.TakeDamage(Random.Range(_minDamage, _maxDamage));
        }

        _audioSource.PlayOneShot(_audio);

        foreach (var effect in _effects)
        {
            effect.transform.parent = null;
            effect.SetActive(true);
        }

        GameObject.Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radius);
    }

    private bool TargetDetector(out List<IDamageable> targets)
    {
        targets = new List<IDamageable>();

        _colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        if (_colliders.Length > 0)
        {
            foreach (var collider in _colliders)
            {
                if (collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    targets.Add(damageable);
                }
            }
        }

        if (targets.Count > 0)
            return true;

        return false;
    }
}