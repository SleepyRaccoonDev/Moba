
using UnityEngine;

public abstract class Controller
{
    private bool _isEnable;

    public virtual void Enable() =>_isEnable = true;
    
    public virtual void Disable() => _isEnable = false;

    public void OnUpdate(Vector3 targetPosition)
    {
        if (_isEnable == false)
            return;

        UpdateInputsLogic(targetPosition);
    }

    public void OnFixedUpdate()
    {
        if (_isEnable == false)
            return;

        UpdateBehaviourLogic();
    }

    protected abstract void UpdateInputsLogic(Vector3 targetPosition);

    protected abstract void UpdateBehaviourLogic();
}