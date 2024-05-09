using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerEnemy : MonoBehaviour
{
    public Transform player; 
    public float speed = 5f;
    public float detectionRadius = 10f;

    private bool isChasing = false;

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        if (distanceToTarget <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Vector3 targetDirection = player.position - transform.position;
            Vector3 moveDirection = targetDirection.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            transform.LookAt(player);
        }
    }
}
