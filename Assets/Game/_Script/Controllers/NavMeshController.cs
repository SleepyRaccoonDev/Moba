using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : Controller
{
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;
    private const float SearchRadius = 10f;
    private const float CheckRadius = 0.15f;

    private IMovable _movable;
    private IJumpable _jumpable;
    private int _agentTypeID;

    private NavMeshPath _path = new NavMeshPath();

    public NavMeshController(IMovable movable, IJumpable jumpable, int agentTypeID)
    {
        StopDistance = movable.Collider.radius + .5f;

        _movable = movable;
        _jumpable = jumpable;
        _agentTypeID = agentTypeID;
    }

    protected override void UpdateInputsLogic(Vector3 targetPosition)
    {
        SetTatgetPoint(targetPosition);
    }

    protected override void UpdateBehaviourLogic()
    {
        _movable.SetMoveDirection(GetDirection());
    }

    protected Vector3 GetDirection()
    {
        if (Vector3.Distance(_movable.Rigidbody.position, TargetPoint) < StopDistance)
        {
            _movable.NavMeshAgent.updatePosition = false;
            return Vector3.zero;
        }

        if (NavMeshUtils.TryGetPath(_movable.Rigidbody.position, TargetPoint, GetNavMeshQueryFilter(), _path) == false
            || NavMeshUtils.IsPathExist(_path) == false)
            return Vector3.zero;

        _movable.NavMeshAgent.updatePosition = true;
        _movable.NavMeshAgent.SetDestination(TargetPoint);

        if (IsOnNavMesh(_movable.Rigidbody.position, out Vector3 point) == false)
        {
            return (point - _movable.Rigidbody.position).normalized;
        }

        if (_movable.NavMeshAgent.isOnOffMeshLink)
        {
            _jumpable.Jump(_movable.NavMeshAgent.currentOffMeshLinkData);
        }

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

    protected virtual void SetTatgetPoint(Vector3 targetPosition)
    {
        TargetPoint = targetPosition;
    }

    private bool IsOnNavMesh(Vector3 fromPosition, out Vector3 nearestPoint)
    {
        if (NavMesh.SamplePosition(fromPosition, out NavMeshHit hit, SearchRadius, NavMesh.AllAreas))
        {
            nearestPoint = hit.position;
            return Vector3.Distance(fromPosition, hit.position) <= CheckRadius;
        }

        nearestPoint = fromPosition;

        return false;
    }
}