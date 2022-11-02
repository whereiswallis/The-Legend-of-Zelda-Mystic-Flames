using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{

    public GameObject player;
    Vector3 mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_pos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // create a plane with center at player whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, player.transform.position);
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0; 
        // if the ray hits the plane...
        if (hPlane.Raycast(ray, out distance)){
            // get the hit point:
            mouse_pos = ray.GetPoint(distance);
            player.transform.LookAt(mouse_pos, Vector3.up);
        }


    }
}
