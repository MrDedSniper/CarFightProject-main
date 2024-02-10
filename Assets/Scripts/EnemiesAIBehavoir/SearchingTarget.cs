using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchingTarget : MonoBehaviour
{
    public float searchRadius = 10f; // Радиус поиска ближайшего игрока
    private NavMeshAgent navAgent;
    public GameObject target;
    private Animator anim;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
            // Повернуться к цели
            transform.LookAt(target.transform);

            // Отправиться к цели
            navAgent.SetDestination(target.transform.position);

            // Проверить, движется ли враг к цели
            if (navAgent.velocity.magnitude > 0.1f)
            {
                anim.SetBool("Moving", true); // Запустить анимацию движения
            }
            else
            {
                anim.SetBool("Moving", false); // Остановить анимацию движения
            }
        }
    }
}
