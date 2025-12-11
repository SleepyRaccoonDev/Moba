using UnityEngine;

public class ResetDamageFlag : MonoBehaviour
{
    [SerializeField] private Character _character;

    public void ResetDamageFlagToFalse()
    {
        _character.IsTakingDamage = false;
    }
}