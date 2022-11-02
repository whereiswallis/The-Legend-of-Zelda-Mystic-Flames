using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanSpawner : MonoBehaviour
{
    public GameObject box;
    public GameObject goal;
    public GameObject chest;
    public GameObject wall;
    public Vector3 playerPosition;
    private GameObject spawnedPlayer;
    private CapsuleCollider playerCollider;
    public Material shaderMaterial;
    public Material originalMaterial;
    private GameObject spawnedChest;
    private int chestSpawn;
    public bool puzzleSolved;
    private bool puzzleFormed = false;
    private bool changeLevel;
    private Vector3 chestLoc;
    private int mapIndex;
    //public GameObject puzzle_instruct;
    //private int goalsMet;
    // private GameObject[] boxes;
    private List<GameObject> boxList;
    // private GameObject[] goals;
    private List<GameObject> goalList;
    private List<GameObject> wallList;


    float transitionTimer;
    bool canMove = true;


    public char[][][] map2 = {
        new char[][] {
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '.', '#', '#', '#', '.', '#'},
            new char[]{'#', '#', ' ', '#', ' ', ' ', '.', '#'},
            new char[]{'#', '#', ' ', '$', '$', '!', '@', '#'},
            new char[]{'#', '#', ' ', ' ', '$', ' ', ' ', '#'},
            new char[]{'#', '#', ' ', ' ', '#', ' ', ' ', '#'},
            new char[]{'#', '#', ' ', ' ', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', ' ', ' ', '.', '@', ' ', '#'},
            new char[]{'#', '#', ' ', '#', '.', '#', ' ', '#'},
            new char[]{'#', '#', ' ', ' ', '!', '$', ' ', '#'},
            new char[]{'#', '#', '.', '$', '$', ' ', '#', '#'},
            new char[]{'#', '#', ' ', ' ', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', ' ', '@', '#', '#'},
            new char[]{'#', '#', '#', '#', ' ', ' ', ' ', '#'},
            new char[]{'#', '.', ' ', '#', '$', '$', ' ', '#'},
            new char[]{'#', ' ', ' ', ' ', ' ', ' ', '#', '#'},
            new char[]{'#', '.', ' ', '!', '$', '#', '#', '#'},
            new char[]{'#', '#', '.', ' ', ' ', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', ' ', '.', '.', '#', '#', '#', '#'},
            new char[]{'#', ' ', '$', ' ', '!', ' ', ' ', '#'},
            new char[]{'#', ' ', ' ', '#', '$', '#', ' ', '#'},
            new char[]{'#', ' ', '@', ' ', '.', '$', ' ', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '.', ' ', '!', '@', '.', '#', '#'},
            new char[]{'#', ' ', ' ', '$', '#', ' ', ' ', '#'},
            new char[]{'#', ' ', '#', ' ', '$', '.', ' ', '#'},
            new char[]{'#', ' ', ' ', ' ', '$', '#', ' ', '#'},
            new char[]{'#', '#', '#', '#', ' ', ' ', ' ', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '.', ' ', '.', '#', '#', '#', '#'},
            new char[]{'#', '.', '#', '$', '$', ' ', '#', '#'},
            new char[]{'#', ' ', '!', ' ', '@', ' ', '#', '#'},
            new char[]{'#', ' ', '$', '#', ' ', ' ', '#', '#'},
            new char[]{'#', '#', ' ', ' ', ' ', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', ' ', ' ', '#'},
            new char[]{'#', '#', '#', '#', '#', '$', '.', '#'},
            new char[]{'#', '#', '#', ' ', ' ', '.', ' ', '#'},
            new char[]{'#', '#', '#', '!', ' ', '#', '.', '#'},
            new char[]{'#', ' ', '$', ' ', ' ', '$', ' ', '#'},
            new char[]{'#', ' ', ' ', ' ', '#', '@', ' ', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', ' ', '!', ' ', '@', '#', '#'},
            new char[]{'#', '#', ' ', ' ', '#', ' ', ' ', '#'},
            new char[]{'#', '#', '.', ' ', ' ', '$', ' ', '#'},
            new char[]{'#', '#', ' ', '$', '$', '#', '.', '#'},
            new char[]{'#', '#', '#', '#', ' ', ' ', '.', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', '#', '#', ' ', ' ', ' ', '#', '#'},
            new char[]{'#', '#', '#', ' ', '#', '.', '#', '#'},
            new char[]{'#', '#', '#', ' ', '!', '.', '#', '#'},
            new char[]{'#', '@', ' ', '$', '$', ' ', '#', '#'},
            new char[]{'#', ' ', ' ', '.', '$', ' ', '#', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        },
        new char[][]{
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'},
            new char[]{'#', ' ', ' ', ' ', '#', ' ', ' ', '#'},
            new char[]{'#', ' ', '#', '.', '$', '!', '$', '#'},
            new char[]{'#', ' ', ' ', ' ', '$', ' ', ' ', '#'},
            new char[]{'#', '#', '#', '#', '#', '.', ' ', '#'},
            new char[]{'#', '#', '#', ' ', ' ', ' ', '@', '#'},
            new char[]{'#', '#', '#', ' ', ' ', ' ', '.', '#'},
            new char[]{'#', '#', '#', '#', '#', '#', '#', '#'}
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        
        //setPuzzle();
        

    }

    // Update is called once per frame
    void Update()
    {


        if(!puzzleFormed){
            setPuzzle(false);
        } else {
        
            int goalsMet = 0;

            for (int i = 0; i < boxList.Count; i++) {
                GameObject boxChild1 = boxList[i].transform.GetChild(0).gameObject;
                GameObject boxChild2 = boxList[i].transform.GetChild(1).gameObject;
                GameObject boxChild3 = boxList[i].transform.GetChild(2).gameObject;
                if(boxList[i].transform.position.z == goalList[0].transform.position.z &&
                        boxList[i].transform.position.x == goalList[0].transform.position.x){
                    boxChild1.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild2.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild3.GetComponent<MeshRenderer>().material = shaderMaterial;
                    goalsMet++;
                } else if(boxList[i].transform.position.z == goalList[1].transform.position.z &&
                        boxList[i].transform.position.x == goalList[1].transform.position.x){
                    boxChild1.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild2.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild3.GetComponent<MeshRenderer>().material = shaderMaterial;
                    goalsMet++;
                } else if(boxList[i].transform.position.z == goalList[2].transform.position.z &&
                        boxList[i].transform.position.x == goalList[2].transform.position.x){
                    boxChild1.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild2.GetComponent<MeshRenderer>().material = shaderMaterial;
                    boxChild3.GetComponent<MeshRenderer>().material = shaderMaterial;
                    goalsMet++;
                } else {
                    boxChild1.GetComponent<MeshRenderer>().material = originalMaterial;
                    boxChild2.GetComponent<MeshRenderer>().material = originalMaterial;
                    boxChild3.GetComponent<MeshRenderer>().material = originalMaterial;
                }
                
            }


            if(goalsMet == goalList.Count && chestSpawn == 1) {
                Debug.Log("Puzzle Solved!!");
                puzzleSolved = true;
                spawnedChest.SetActive(true);
                chestSpawn = 0;
            } else if(chestSpawn == 0 && goalsMet != goalList.Count && spawnedChest != null){
                spawnedChest.SetActive(false);
                chestSpawn = 1;
            }
            
            if(Input.GetKeyDown(KeyCode.R) && !puzzleSolved){
                destroyPuzzle();
                setPuzzle(true);
                spawnedPlayer.GetComponent<Transform>().position = playerPosition;
                GameObject.Find("Generate").GetComponent<Generate>().setPuzzleInstruct();
                //puzzle_instruct.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
                transitionTimer = Time.time + 3f;
                GameObject.Find("Character").GetComponent<ThirdPersonMovement>().enabled = false;
                canMove = false;
                Invoke("teleport", 1.5f);
            }


            if (Time.time> transitionTimer && !canMove)
            {
                GameObject.Find("Character").GetComponent<ThirdPersonMovement>().enabled = true;
            }
        }
    }

    private void destroyPuzzle()
    {
        for (int i = boxList.Count - 1; i >= 0; i--) {
            Destroy(boxList[i]);
            boxList.RemoveAt(i);
                
        }

        for (int j = wallList.Count - 1; j >= 0; j--) {
            Destroy(wallList[j]);
            wallList.RemoveAt(j);
                
        }

        for (int k = goalList.Count - 1; k >= 0; k--) {
            Destroy(goalList[k]);
            goalList.RemoveAt(k);
        }

        if(spawnedChest.activeSelf == true){
            spawnedChest.SetActive(false);
        }
        chestSpawn = 1;

        puzzleFormed = false;
    }

    private void setPuzzle(bool samePuzzle)
    {
        spawnedPlayer = GameObject.Find("Character");
        boxList = new List<GameObject>();
        goalList = new List<GameObject>();
        wallList = new List<GameObject>();
        puzzleSolved = false;
        //puzzle_instruct = GameObject.Find("Puzzle Instruct");
        if(!samePuzzle){
            mapIndex = Random.Range(0,9);
        }
        chestSpawn = 1;
        Debug.Log(mapIndex);

        GameObject puzzleRoom = GameObject.Find("level");
        Debug.Log(transform.parent.gameObject.transform.position);
        // int offsetX = -14;
        // int offsetY = 14;
        float offsetX = transform.parent.gameObject.transform.position.x - (float)14;
        float offsetY = transform.parent.gameObject.transform.position.z + 14;
        float y = transform.parent.gameObject.transform.position.y + 2;
        int dec = 4;
        
        for(int j = 0; j < map2[mapIndex].Length; j++){
            for(int k = 0; k < map2[mapIndex][j].Length; k++){
                float x1 = offsetX + (k * dec);
                float z2 = offsetY;
                Vector3 wallPos = new Vector3(x1, y, z2);
                if(map2[mapIndex][j][k] == '#'){
                    GameObject puzzleWall = Instantiate(wall, wallPos, GetComponent<Transform>().rotation);
                    puzzleWall.transform.SetParent(puzzleRoom.transform, false);
                    wallList.Add(puzzleWall);
                } else if(map2[mapIndex][j][k] == '.'){
                    GameObject puzzleGoal = Instantiate(goal, wallPos, GetComponent<Transform>().rotation);
                    puzzleGoal.transform.SetParent(puzzleRoom.transform, false);
                    goalList.Add(puzzleGoal);
                } else if(map2[mapIndex][j][k] == '$'){
                    wallPos.y = y - (float)0.5;
                    GameObject puzzleBox = Instantiate(box, wallPos, GetComponent<Transform>().rotation);
                    puzzleBox.transform.SetParent(puzzleRoom.transform, false);
                    boxList.Add(puzzleBox);
                } else if(map2[mapIndex][j][k] == '@'){
                    wallPos.y = y-2;
                    playerPosition = wallPos;
                    //spawnedPlayer = Instantiate(player, wallPos, GetComponent<Transform>().rotation);
                } else if(map2[mapIndex][j][k] == '!'){
                    wallPos.y = y-2;
                    chestLoc = wallPos;
                    //spawnedChest = Instantiate(chest, chestLoc, Quaternion.Inverse(chest.transform.rotation));
                    spawnedChest = Instantiate(chest, chestLoc, Quaternion.identity);
                    spawnedChest.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    spawnedChest.transform.SetParent(puzzleRoom.transform, false);
                    spawnedChest.SetActive(false);
                }
            }
            offsetY = offsetY - dec;
        }

        puzzleFormed = true;
    }

    private void OnDestroy() {
        if(puzzleFormed){
            destroyPuzzle();
        }
    }

    private void OnDisable() {
        if(puzzleFormed){
            destroyPuzzle();
        }
    }

    public void teleport()
    {
        playerCollider = spawnedPlayer.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>();
        playerCollider.isTrigger = false;
        spawnedPlayer.transform.position = GameObject.Find("puzzleEntryPoint").transform.position;
        if(!puzzleSolved){
            destroyPuzzle();
            setPuzzle(true);
        }
        
    }
}
