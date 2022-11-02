using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevel : MonoBehaviour
{
    public void destroyLevel()
    {
        Destroy(GameObject.Find("level"));
    }
}
