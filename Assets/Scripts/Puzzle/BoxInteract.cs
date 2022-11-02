using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    private string movement;
    public float range = 2;
    public float playerRange = 5;
    private GameObject[] boxes;
    private GameObject player;
    private Ray ray1;
    private Ray ray2;
    private Ray ray3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            movement = "Moving forward";
        } else if(Input.GetKeyDown(KeyCode.D)){
            movement = "Moving Right";
        } else if(Input.GetKeyDown(KeyCode.A)){
            movement = "Moving Left";
        } else if(Input.GetKeyDown(KeyCode.S)){
            movement = "Moving Back";
        }

    }

    void OnTriggerEnter(Collider player) {
        GameObject sokoban = GameObject.Find("SokobanSpawner");
        bool puzzleSolved = sokoban.GetComponent<SokobanSpawner>().puzzleSolved;
        Debug.Log(player.tag);
        bool hit2 = checkCollision();
        
        Vector3 pos = GetComponent<Transform>().position;
        if(!puzzleSolved && player.tag == "Player" && !hit2){
            if(string.Compare(movement, "Moving forward") == 0){
                pos.z = (int)(pos.z + 1);
                GetComponent<Transform>().position = pos;
            } else if(string.Compare(movement, "Moving Right") == 0){
                pos.x = (int)(pos.x + 1);
                GetComponent<Transform>().position = pos;
            } else if(string.Compare(movement, "Moving Left") == 0){
                pos.x = (int)(pos.x - 1);
                GetComponent<Transform>().position = pos;
            } else if(string.Compare(movement, "Moving Back") == 0){
                pos.z = (int)(pos.z - 1);
                GetComponent<Transform>().position = pos;
            }
        }

        
    }

    void OnTriggerStay(Collider player) {
        if(player.tag == "Player"){
        List<Ray> playerRays = getPlayerRay(player.gameObject);
            if(Physics.Raycast(playerRays[0], out RaycastHit hit, range) && 
                    Physics.Raycast(playerRays[1], out RaycastHit hit2, range)) {
                if (hit.collider.tag == "PuzzleBox" && hit2.collider.tag == "Wall") {
                    bool hit3 = checkCollision();                    
                    Vector3 pos = GetComponent<Transform>().position;
                    if(player.tag == "Player" && !hit3){
                        if(string.Compare(movement, "Moving forward") == 0){
                            pos.z = (int)(pos.z + 1);
                            GetComponent<Transform>().position = pos;
                        } else if(string.Compare(movement, "Moving Right") == 0){
                            pos.x = (int)(pos.x + 1);
                            GetComponent<Transform>().position = pos;
                        } else if(string.Compare(movement, "Moving Left") == 0){
                            pos.x = (int)(pos.x - 1);
                            GetComponent<Transform>().position = pos;
                        } else if(string.Compare(movement, "Moving Back") == 0){
                            pos.z = (int)(pos.z - 1);
                            GetComponent<Transform>().position = pos;
                        }
                    }
                }
            }
        }
        
        

    }

    private bool checkCollision()
    {
        bool hit2 = false;
        List<Ray> rayList = getRayList();
        foreach(Ray newRay in rayList){
            if(Physics.Raycast(newRay, out RaycastHit hit, range)) {
                if (hit.collider.tag == "PuzzleBox" || hit.collider.tag == "Wall") {
                    hit2 = true;
                }
            }
        }

        return hit2;
    }

    private List<Ray> getPlayerRay(GameObject player)
    {
        List<Ray> rayList = new List<Ray>();
        Vector3 direction = Vector3.forward;
        Vector3 direction2 = Vector3.back;
        Vector3 pos = player.transform.position;
        
        if(string.Compare(movement, "Moving forward") == 0){
            direction = Vector3.forward;
            direction2 = Vector3.back;
        } else if(string.Compare(movement, "Moving Right") == 0){
            direction = Vector3.right;
            direction2 = Vector3.left;
        } else if(string.Compare(movement, "Moving Left") == 0){
            direction = Vector3.left;
            direction2 = Vector3.right;
        } else if(string.Compare(movement, "Moving Back") == 0){
            direction = Vector3.back;
            direction2 = Vector3.forward;
        }

        rayList.Add(new Ray(pos, GetComponent<Transform>().TransformDirection(direction * range)));
        rayList.Add(new Ray(pos, GetComponent<Transform>().TransformDirection(direction2 * range)));

       return rayList;

    }

    private List<Ray> getRayList()
    {
        List<Ray> rayList = new List<Ray>();
        Vector3 direction = Vector3.forward;
        Vector3 pos = GetComponent<Transform>().position;
        Vector3 pos2 = GetComponent<Transform>().position;
        Vector3 pos3 = GetComponent<Transform>().position;
        if(string.Compare(movement, "Moving forward") == 0){
            pos2.x = pos.x + (float)1.5;
            pos3.x = pos.x - (float)1.5;
            direction = Vector3.forward;
        } else if(string.Compare(movement, "Moving Right") == 0){
            pos2.z = pos.z + (float)1.5;
            pos3.z = pos.z - (float)1.5;
            direction = Vector3.right;
        } else if(string.Compare(movement, "Moving Left") == 0){
            pos2.z = pos.z + (float)1.5;
            pos3.z = pos.z - (float)1.5;
            direction = Vector3.left;
        } else if(string.Compare(movement, "Moving Back") == 0){
            pos2.x = pos.x + (float)1.5;
            pos3.x = pos.x - (float)1.5;
            direction = Vector3.back;
        }
       rayList.Add(new Ray(pos, GetComponent<Transform>().TransformDirection(direction * range)));
       rayList.Add(new Ray(pos2, GetComponent<Transform>().TransformDirection(direction * range)));
       rayList.Add(new Ray(pos3, GetComponent<Transform>().TransformDirection(direction * range)));

       return rayList;
    }

}
