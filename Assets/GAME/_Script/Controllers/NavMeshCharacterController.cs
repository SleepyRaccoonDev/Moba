using UnityEngine;
using UnityEngine.AI;

public class NavMeshCharacterController : Controller
{
    private const string KeyForRayMask = "Floor";
    private const float StopDistance = 0.3f;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private readonly int _maxDistanceForRay = 200;

    private Character _character;
    private int _agentTypeID;

    private NavMeshPath _path = new NavMeshPath();
    private Vector3 _targetPoint;

    private Camera _camera;

    private RaycastHit _hit;
    private LayerMask _mask;

    private GameObject _pointView;

    public NavMeshCharacterController(Character character, int agentTypeID, GameObject pointPrefab)
    {
        _character = character;
        _agentTypeID = agentTypeID;
        _camera = Camera.main;

        _mask = LayerMask.GetMask(KeyForRayMask);

        _pointView = GameObject.Instantiate(pointPrefab);
        ShowPointView(false);
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {
        SetTatgetPoint(targetPosition);
    }

    protected override void UpdateBehaviourLogic()
    {
        _character.UpdateBehaviors(GetDirection());
    }

    private Vector3 GetDirection()
    {
        if (Vector3.Distance(_character.Rigidbody.position, _targetPoint) < StopDistance)
        {
            ShowPointView(false);
            return Vector3.zero;
        }

        if (NavMeshUtils.TryGetPath(_character.Rigidbody.position, _targetPoint, GetNavMeshQueryFilter(), _path) == false
            || NavMeshUtils.IsPathExist(_path) == false)
            return Vector3.zero;

        return GetCharacterDirection();
    }

    private NavMeshQueryFilter GetNavMeshQueryFilter()
    {
        NavMeshQueryFilter filter = new NavMeshQueryFilter();
        filter.agentTypeID = _agentTypeID;
        filter.areaMask = NavMesh.AllAreas;

        return filter;
    }

    private Vector3 GetCharacterDirection()
    {
        return (_path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex]).normalized;
    }

    private void SetTatgetPoint(Vector3 targetPosition)
    {
        Ray ray = _camera.ScreenPointToRay(targetPosition);

        if (Physics.Raycast(ray, out _hit, _maxDistanceForRay, _mask) == false)
        {
            _targetPoint = Vector3.zero;
        }
        else
        {
            ShowPointView(true);
            _pointView.transform.position = _hit.point;
            _targetPoint = _hit.point;
        }
    }

    private void ShowPointView(bool value)
    {
        _pointView.gameObject.SetActive(value);
    }
}