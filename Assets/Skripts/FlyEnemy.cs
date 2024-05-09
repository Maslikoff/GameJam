using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public List<Transform> targetPoints = new List<Transform>();
    public float speed = 5f;

    private int _currentTargetIndex = 0;

    void Start()
    {
        if (targetPoints.Count > 0)
        {
            transform.position = targetPoints[_currentTargetIndex].position;
        }
    }

    void Update()
    {
        if (targetPoints.Count == 0)
        {
            return;
        }

        Vector3 currentTargetPosition = targetPoints[_currentTargetIndex].position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, step);

        if (Vector3.Distance(transform.position, currentTargetPosition) < 0.1f)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % targetPoints.Count;
        }
    }
}
