using UnityEngine;

public class TargetPointController : IViewBehaviour
{
    private GameObject _targetPointView;
    private Controller _controller;
    private IMovable _movable;

    public TargetPointController(IMovable movable, Controller controller, GameObject targetPointPrefab)
    {
        _movable = movable; 
        _controller = controller;
        _targetPointView = GameObject.Instantiate(targetPointPrefab, Vector3.zero, Quaternion.identity);
        ShowPointView(false);
    }

    public void Perform()
    {
        if (Vector3.Distance(_movable.Rigidbody.position, _controller.TargetPoint) > _controller.StopDistance)
        {
            ShowPointView(true);
            _targetPointView.transform.position = _controller.TargetPoint;
        }
        else
        {
            ShowPointView(false);
        }
    }

    private void ShowPointView(bool value)
    {
        _targetPointView.gameObject.SetActive(value);
    }
}