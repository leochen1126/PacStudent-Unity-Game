using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryMovement : MonoBehaviour
{
    private Vector2 targetPoint;
    private float speed;

    public void SetTarget(Vector2 target, float cherrySpeed)
    {
        targetPoint = target;
        speed = cherrySpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        if ((Vector2)transform.position == targetPoint || OutOfCameraView())
        {
            Destroy(gameObject);
        }
    }

    private bool OutOfCameraView()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }
}
