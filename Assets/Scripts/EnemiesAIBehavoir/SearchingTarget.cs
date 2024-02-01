using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchingTarget : MonoBehaviour
{
    public float searchRadius = 10f; // радиус поиска ближайшего игрока
    private NavMeshAgent navAgent;
    public GameObject target;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PlayerCar"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = collider.gameObject;
                }
            }
        }

        if (target != null)
        {
            // повернуться к цели
            transform.LookAt(target.transform);

            // отправиться к цели
            navAgent.SetDestination(target.transform.position);
        }
    }
}
