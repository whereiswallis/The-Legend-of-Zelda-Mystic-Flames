using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHover : MonoBehaviour
{
    private const float threshold = 0.02f;

    private Vector3 startingPos;
    private Vector3 destinationPos;
    private Vector3 velocity;
    private int alt;
    public float radius = 0.3f;
    public float maxTimeDelay = 0.5f;

    private void Start() {
        startingPos = destinationPos = transform.position;
        alt = 1;
    }


    public void FixedUpdate() {

        if ( Vector3.Distance(transform.position, destinationPos) < threshold ) {
            Vector3 dest = Random.insideUnitSphere;
            if(alt == 1) {
                dest.x = dest.z = 0;
                dest.y = 1f;
                destinationPos = startingPos + dest * radius;
                alt = 0;
            }else if(alt == 0) {
                dest.x = dest.z = 0;
                dest.y = -1f;
                destinationPos = startingPos + dest * radius;
                alt = 1;
            }
            velocity = (destinationPos - transform.position) / maxTimeDelay;
        }

        transform.position += velocity * Time.fixedDeltaTime;
    }
    void Update()
    {
        
    }

}
