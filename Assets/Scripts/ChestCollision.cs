using UnityEngine;

public class ChestCollision : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject FloatingTextPrefab;
    [SerializeField] private GameObject AbilitySelection;
    private bool canOpen = false;
    private bool isOpen = false;
    private bool canSelect = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) & canOpen)
        {
            anim.Play("ChestOpen");
            isOpen = true;
            FloatingTextPrefab.SetActive(false);
            if(!canSelect)
            {
                canSelect = true;
                ShowAbility();
            }
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" & !isOpen)
        {
            Debug.Log("Can interact!");
            FloatingTextPrefab.SetActive(true);
            canOpen = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" & !isOpen)
        {
            FloatingTextPrefab.SetActive(false);
            canOpen = false;
        }
    }

    void ShowAbility()
    {
        AbilitySelection.GetComponent<AbilitySelectionMenu>().GenerateChest();
        AbilitySelection.SetActive(true);
    }

    public void Continue()
    {
        AbilitySelection.SetActive(false);
    }
}
