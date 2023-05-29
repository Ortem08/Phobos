using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
    }
}
