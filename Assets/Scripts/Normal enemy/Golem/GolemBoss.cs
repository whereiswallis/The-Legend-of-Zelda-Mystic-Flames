using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemBoss : MonoBehaviour
{
    public GameObject Tiny;
    public GameObject bossRoom;

    private void Start() {
        bossRoom = GameObject.Find("boss");    
    }
    
    public void splitTiny()
    {
        GameObject Tiny1 = Instantiate(Tiny, gameObject.transform.position + (new Vector3(6, 0, 6)), 
            Quaternion.Euler(Generate.Facing[Random.Range(0,4)]));
        Tiny1.transform.SetParent(bossRoom.transform, true);
        GameObject Tiny2 = Instantiate(Tiny, gameObject.transform.position + (new Vector3(-6, 0, 6)), 
            Quaternion.Euler(Generate.Facing[Random.Range(0, 4)]));
        Tiny2.transform.SetParent(bossRoom.transform, true);
        GameObject Tiny3 = Instantiate(Tiny, gameObject.transform.position + (new Vector3(0, 0, 12)), 
            Quaternion.Euler(Generate.Facing[Random.Range(0, 4)]));
        Tiny3.transform.SetParent(bossRoom.transform, true);
    }
}
