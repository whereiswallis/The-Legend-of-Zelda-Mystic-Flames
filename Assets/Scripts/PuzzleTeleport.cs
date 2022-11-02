using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTeleport : MonoBehaviour
{
    private bool isInside = false;
    private Generate Generate;
    private CapsuleCollider playerCollider;
    public GameObject puzzle_instruct;
    float timer;
    bool canMove = true;


    private void Start()
    {
        Generate = GameObject.Find("Generate").GetComponent<Generate>();
        Generate.setPuzzleHintOff();
        //puzzle_instruct = GameObject.Find("Puzzle Instruct");


        Generate.setPuzzleInstructOff();
        //puzzle_instruct.SetActive(false);
        //hint = GameObject.Find("ProceedHint");
        //hint.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInside)
        {
            isInside = false;
            Generate.setPuzzleHint();
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
            GameObject.Find("Character").GetComponent<ThirdPersonMovement>().enabled = false;
            Invoke("teleport", 1.5f);
            timer = Time.time + 3f;
            canMove = false;
        }


        if (Time.time > timer && !canMove)
        {
            GameObject.Find("Character").GetComponent<ThirdPersonMovement>().enabled = true;
            canMove = true;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInside = true;
            Generate.setPuzzleHint();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInside = false;
            Generate.setPuzzleHintOff();
        }
    }

    public void setHint()
    {
        //hint.SetActive(true);
        Generate.setHint();
    }

    public void delayDestroy()
    {
        Destroy(GameObject.Find("boss"));

    }

    public void teleport()
    {
        GameObject player = GameObject.Find("Character");
        playerCollider = player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>();
        playerCollider.isTrigger = true;
        player.transform.position = GameObject.Find("SokobanSpawner").GetComponent<SokobanSpawner>().playerPosition;
        Generate.setPuzzleInstruct();
    }
}
