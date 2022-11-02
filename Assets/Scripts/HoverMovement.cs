using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    private float multiplier = 0.2f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time)*multiplier + 0.5f, transform.position.z);
    }
}
