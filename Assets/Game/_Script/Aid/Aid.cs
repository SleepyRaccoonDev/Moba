using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Aid : MonoBehaviour
{
    private BoxCollider _collider;
    private Rigidbody _rigitbody;
    private AudioSource _audioSource;

    private float _healValue = 10;
    private float _timeToDestroy = 20;

    private AudioClip _audio;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigitbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(AudioSource audioSource, float healValue, AudioClip audio)
    {
        _healValue = healValue;
        _audioSource = audioSource;
        _audio = audio;

        DestroyAid(_timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigitbody.isKinematic = _collider.isTrigger  = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealable>(out IHealable healable))
        {
            Activate(healable);
        }    
    }

    private void DestroyAid(float time)
    {
        GameObject.Destroy(this.gameObject, time);
    }

    private void Activate(IHealable healable)
    {
        _audioSource.PlayOneShot(_audio);
        healable.Heal(_healValue);
        DestroyAid(0);
    }
}