using CodeBase.Player;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;
    private EnemyReferences _enemyReferences;
    private float _pathUpdateDeadline;
    private float _shootingDistance;

    private void Awake()
    {
        _enemyReferences = GetComponent<EnemyReferences>();
    }


    void Start()
    {
        _shootingDistance = _enemyReferences.navMeshAgent.stoppingDistance;
    }


    private void Update()
    {
        if(target != null)
        {
            bool isRange = Vector3.Distance(transform.position, target.position) <= _shootingDistance;

            if(isRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
        }
    }



    private void LookAtTarget()
    {
        Vector3 lookPosition = target.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f);
    }
    
    private void UpdatePath()
    {
        if (Time.time >= _pathUpdateDeadline)
        {
            _pathUpdateDeadline = Time.time + _enemyReferences.pathUpdateDelay;
            _enemyReferences.navMeshAgent.SetDestination(target.position);
        }
    }
}
