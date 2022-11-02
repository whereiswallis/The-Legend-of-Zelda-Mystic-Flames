using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject shatteredVersion;
    public GameObject potion;
    public GameObject gold;
    public bool isHit = false;
    private float itemThreshold = 0.5f;
    private float potionThreshold = 0.5f;
    private float goldThreshold = 0.3f;
    private float itemDiscovery;
    private float chance = -1f;

    void Start()
    {

    }

    void Update()
    {
        //if((Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.E)) & isHit)
        //{
        //    Instantiate(shatteredVersion, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //    chance = Random.Range(0f, 1f);
        //    if (chance > potionThreshold)
        //    {
        //        GameObject child = Instantiate(potion, transform.position, potion.transform.rotation);
        //        child.transform.SetParent(GameObject.Find("level").transform, false);
        //    }
        //    else if(chance > goldThreshold)
        //    {
        //        GameObject child = Instantiate(gold, transform.position, gold.transform.rotation);
        //        child.transform.SetParent(GameObject.Find("level").transform, false);
        //    }
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            isHit = true;
            Debug.Log("Hit");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
        {
            isHit = false;
            Debug.Log("Not hit");
        }
    }

    public void hitCrate()
    {
        Instantiate(shatteredVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        itemDiscovery = Random.Range(0f, 1f);
        if(itemDiscovery > itemThreshold)
        {
            chance = Random.Range(0f, 1f);
            if (chance > potionThreshold)
            {
                GameObject child = Instantiate(potion, transform.position, potion.transform.rotation);
                child.transform.SetParent(GameObject.Find("level").transform, false);
            }
            else if (chance > goldThreshold)
            {
                GameObject child = Instantiate(gold, transform.position, gold.transform.rotation);
                child.transform.SetParent(GameObject.Find("level").transform, false);
            }
        }
    }
}
