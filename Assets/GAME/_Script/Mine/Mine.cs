using UnityEngine;

public class Mine : MonoBehaviour
{
    private const int FirstInput = 0;

    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject[] _effects;

    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;

    private Collider[] _colliders;

    private void Update()
    {
        _colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        if (_colliders.Length > 0)
        {
            if (_colliders[FirstInput].gameObject.TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(Random.Range(_minDamage, _maxDamage));

                foreach (var effect in _effects)
                {
                    effect.transform.parent = null;
                    effect.SetActive(true);
                }

                GameObject.Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}