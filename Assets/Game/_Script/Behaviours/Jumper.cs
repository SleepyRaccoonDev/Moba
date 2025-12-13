using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Jumper
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private AnimationCurve _jumpCurve;
    private IJumpable _jumpable;

    public Jumper(IJumpable jumpable, Rigidbody rigidbody, NavMeshAgent navMeshAgent, AnimationCurve jumpCurve)
    {
        _jumpable = jumpable;
        _rigidbody = rigidbody;
        _navMeshAgent = navMeshAgent;
        _jumpCurve = jumpCurve;
    }

    public IEnumerator Jump(OffMeshLinkData linkData)
    {
        var end = linkData.endPos;

        var duration = Vector3.Distance(_rigidbody.transform.position, end) / 5;

        float process = 0;

        while (process < duration)
        {
            float yOffset = _jumpCurve.Evaluate(process/ duration);

            _rigidbody.transform.position = Vector3.Lerp(_rigidbody.transform.position, end, process / duration) + Vector3.up * yOffset;
            process += Time.deltaTime;

            yield return null;
        }

        _navMeshAgent.CompleteOffMeshLink();

        _jumpable.ResetJump();
    }
}
