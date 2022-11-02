using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class roomControlTut : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> doors = new List<GameObject>();
    [SerializeField]public bool hasEntered = false;
    public GameObject proceedPoint;
    public static int numGolem = 13;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectwithTag(enemies, "Enemy");
        FindObjectwithTag(doors, "Door");
        foreach (GameObject enemy in enemies)
        {

            enemy.GetComponent<Animator>().enabled = false;

        }

        
        //FindObjectWithTag(doors, "Door");
    }

    // Update is called once per frame
    void Update()
    {


        if (hasEntered && enemies.Count != 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<BoxCollider>().enabled = false;
            }
        }

        else if(enemies.Count == 0)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<BoxCollider>().enabled = true;
            }
        }


        if (hasEntered){
            foreach (GameObject enemy in enemies)
            {

                enemy.GetComponent<Animator>().enabled = true;

            }
        }
    }
    public void FindObjectwithTag(List<GameObject> lst, string searchTag)
    {
        lst.Clear();
        Transform parent = transform;
        GetChildObject(lst,parent, searchTag);
    }
 

    public void GetChildObject(List<GameObject> lst, Transform parent, string searchTag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == searchTag)
            {
                lst.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(lst,child, searchTag);
            }
        }
    }



    public void PlayerEnter()
    {
        hasEntered = true;
    }



}
