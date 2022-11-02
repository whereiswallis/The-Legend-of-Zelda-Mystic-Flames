using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using TMPro;

public class Generate : MonoBehaviour
{
    public static bool canProceed = false;
    [SerializeField]
    private int minWidth = 5, minLength = 5;
    [SerializeField]
    private int dungeonWidth = 30, dungeonLength = 30;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private int threshold;
    private List<Vector3Int> Directions = new List<Vector3Int>();
    public static List<Vector3> Facing = new List<Vector3>();
    public static List<Vector3> allObjects;
    public static List<BoundsInt> roomList;
    public GameObject pillarPrefab;
    public GameObject floorPrefab;
    public GameObject wallPrefab1;
    public GameObject wallPrefab2;
    public GameObject[] monsterPrefab;
    public GameObject pillarDecoPrefab;
    public GameObject[] spawnPrefab;
    public GameObject[] cratePrefab;
    public GameObject proceedPrefab;
    public GameObject merchantPrefab;
    public GameObject bossRoomPrefab;
    public GameObject miniBossRoomPrefab;
    public GameObject merchantDialogue;
    public GameObject hint;
    public GameObject puzzleHint;
    public GameObject chestPrefab;
    public GameObject puzzlePrefab;
    public GameObject puzzleTeleportPrefab;
    public GameObject puzzleInstruct;

    public static Vector3 spawnCoordinate;


    public GameObject player;
    public GameObject level_text;

    private float timer;
    private bool isTransition = false;

    float transitionTimer = 0;
    bool canMove = true;

    static BoundsInt spawnRoom;
    static BoundsInt proceedRoom;


    void Start()
    {
        level_text = GameObject.Find("Level Number");
        Directions.Add(new Vector3Int(8, 0, 0));
        Directions.Add(new Vector3Int(-8, 0, 0));
        Directions.Add(new Vector3Int(0, 0, 8));
        Directions.Add(new Vector3Int(0, 0, -8));
        Facing.Add(new Vector3(0, 0, 0));
        Facing.Add(new Vector3(0, 90, 0));
        Facing.Add(new Vector3(0, 180, 0));
        Facing.Add(new Vector3(0, 270, 0));

        CreateRooms();
    }
    void Update()
    {
        if ((GameStatistics.level != 5) && (GameStatistics.level != 10)){
            level_text.GetComponent<TextMeshProUGUI>().text = "Level " + (GameStatistics.level + 1);
        }
        else if (GameStatistics.level == 10){
            level_text.GetComponent<TextMeshProUGUI>().text = "Final Battle";
        }
        else {
            level_text.GetComponent<TextMeshProUGUI>().text = "Rocky Balboulder";
        }

        if (GameObject.Find("Character"))
            player = GameObject.Find("Character");

        if (canProceed) //|| Input.GetKeyDown(KeyCode.Q))
        {
            timer = Time.time + 1.5f;
            transitionTimer = Time.time + 3f;
            canProceed = false;
            isTransition = true;
            player.GetComponentInParent<ThirdPersonMovement>().enabled = false;
            canMove = false;
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
            //player.GetComponent<PlayerHealth_modified>().setInvincible(true);
        }
        if (Time.time > timer)
        {
            if(isTransition && GameStatistics.level < GameStatistics.final)
            {
                //GameObject.Find("Objective").GetComponent<Proceed>().Invoke("setHint", 1.5f);

                //GameObject.Find("Merchant Trigger").GetComponent<MerchantTrigger>().Invoke("setMerchant", 1.5f);
                setHint();
                setMerchant();
                destroyLevel();
                destroyBossRoom();

                GameStatistics.level++;
                
                
                CreateRooms();

                //Invoke("setHint", 1.5f);
                //Invoke("setMerchant", 1.5f);
                //Invoke("destroyLevel", 1.5f);
                //Invoke("destroyBossRoom", 1.5f);
                //destroyLevel();
                
                //Invoke("CreateRooms", 1.5f);
                //CreateRooms();
                isTransition = false;
                
                //player.GetComponent<PlayerHealth_modified>().setInvincible(false);
            }

        }

        if (Time.time > transitionTimer && !canMove)
        {
            player.GetComponentInParent<ThirdPersonMovement>().enabled = true;
            canMove = true;
        }

        
    }

    public void CreateRooms()
    {
        if(GameStatistics.level == 10)
        {
            Vector3Int coordinate = new Vector3Int(800, 0, 800);
            GameObject finalRoom = Instantiate(bossRoomPrefab, coordinate, Quaternion.identity);
            finalRoom.name = "boss";
            finalRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            finalRoom.AddComponent<NavMeshSurface>();
            finalRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
            GameObject.Find("Character").transform.position = new Vector3(818, 0, 726);
            return;
        }

        if (GameStatistics.level == 5)
        {
            Vector3Int coordinate = new Vector3Int(1600, 0, 1600);
            GameObject middleRoom = Instantiate(miniBossRoomPrefab, coordinate, Quaternion.identity);
            middleRoom.name = "boss";
            middleRoom.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            middleRoom.AddComponent<NavMeshSurface>();
            middleRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
            GameObject.Find("Character").transform.position = new Vector3(1618, 0, 1526);
            return;
        }

        allObjects = new List<Vector3>();
        if(GameStatistics.level < 4)
        {
            dungeonLength = 20;
            dungeonWidth = 20;
            threshold = 8;
        }
        else if(GameStatistics.level < 7)
        {
            dungeonLength = 30;
            dungeonWidth = 30;
            threshold = 12;
        }
        else if(GameStatistics.level < 10)
        {
            dungeonLength = 40;
            dungeonWidth = 40;
            threshold = 20;
        }
        else if(GameStatistics.level == 10)
        {
            dungeonLength = 50;
            dungeonWidth = 50;
            threshold = 25;
        }


        roomList = BSP.partition(new BoundsInt(Vector3Int.zero, new Vector3Int(dungeonWidth, 0, dungeonLength)), minWidth, minLength);
        while(roomList.Count != threshold)
        {
            roomList = BSP.partition(new BoundsInt(Vector3Int.zero, new Vector3Int(dungeonWidth, 0, dungeonLength)), minWidth, minLength);
        }
        HashSet<Vector3Int> allGrounds = new HashSet<Vector3Int>();
        HashSet<Vector3Int> paths = new HashSet<Vector3Int>();
        List<Vector3Int> roomCenters = new List<Vector3Int>();
        Vector3Int topLeftPillarPosition;
        Vector3Int botLeftPillarPosition;
        Vector3Int topRightPillarPosition;
        Vector3Int botRightPillarPosition;
        List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
        GameObject Level = new GameObject("level");
        GameObject puzzleRoom = Instantiate(puzzlePrefab, new Vector3Int(-100, 0, -100), Quaternion.identity);
        puzzleRoom.name = "puzzleRoom";
        puzzleRoom.transform.SetParent(Level.transform, false);

        int roomNumber = 0;
        foreach(var room in roomList)
        {
            print(room.ToString());
            HashSet<Vector3Int> ground = new HashSet<Vector3Int>();
            topLeftPillarPosition = (new Vector3Int(room.xMin, 0, room.zMax)) * 8 + new Vector3Int(3, 0 , -11);
            botLeftPillarPosition = (new Vector3Int(room.xMin, 0, room.zMin)) * 8 + new Vector3Int(3, 0, 3);
            topRightPillarPosition = (new Vector3Int(room.xMax, 0, room.zMax)) * 8 + new Vector3Int(-11, 0, -11);
            botRightPillarPosition = (new Vector3Int(room.xMax, 0, room.zMin)) * 8 + new Vector3Int(-11, 0, 3);

            GameObject Room = new GameObject("room_" + roomNumber.ToString());
            GameObject WayPoint = new GameObject("WayPoint");
            WayPoint.transform.position = room.center * 8;
            WayPoint.tag = "Waypoints";
            WayPoint.transform.SetParent(Room.transform, false);
            Room.transform.SetParent(Level.transform, false);


            if (roomNumber == 0)
            {
                spawnRoom = room;
            }

            if(roomNumber == roomList.Count - 1)
            {
                proceedRoom = room;
            }
            proceedPoint(roomNumber, proceedRoom, Level);


            //playerSpawn(roomNumber, room);
            //var flags = StaticEditorFlags.NavigationStatic;
            //GameObjectUtility.SetStaticEditorFlags(Room, flags);
            //GameObjectUtility.SetNavMeshArea(Room, roomNumber);
            GameObject pillarLT = Instantiate(pillarPrefab, topLeftPillarPosition, Quaternion.identity) as GameObject;
            pillarLT.transform.SetParent(Room.transform, false);
            GameObject pillarLB = Instantiate(pillarPrefab, botLeftPillarPosition, Quaternion.identity) as GameObject;
            pillarLB.transform.SetParent(Room.transform, false);
            GameObject pillarRT = Instantiate(pillarPrefab, topRightPillarPosition, Quaternion.identity) as GameObject;
            pillarRT.transform.SetParent(Room.transform, false);
            GameObject pillarRB = Instantiate(pillarPrefab, botRightPillarPosition, Quaternion.identity) as GameObject;
            pillarRB.transform.SetParent(Room.transform, false);


            roomCenters.Add(Vector3Int.RoundToInt(room.center));
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for(int row = offset; row < room.size.z - offset; row++)
                {
                    Vector3Int position = room.min * 8 + new Vector3Int(col * 8, 0, row * 8);
                    allGrounds.Add(position);
                    ground.Add(position);
                }
            }
            foreach (var floor in ground)
            {
                GameObject childObject = Instantiate(floorPrefab, floor, Quaternion.identity) as GameObject;
                childObject.AddComponent<BoxCollider>();
                childObject.GetComponent<BoxCollider>().center = new Vector3(0, -1, 0);
                //childObject.AddComponent<NavMeshSurface>();
                //surfaces.Add(childObject.GetComponent<NavMeshSurface>());
                childObject.transform.SetParent(Room.transform, false);
            }
            Room.AddComponent<NavMeshSurface>();
            surfaces.Add(Room.GetComponent<NavMeshSurface>());
            roomNumber++;
        }


        // Create paths between rooms
        //var currentRoom = roomCenters[Random.Range(0, roomCenters.Count)];
        var currentRoom = roomCenters[0];

        roomCenters.Remove(currentRoom);

        int pathNumber = 0;
        while (roomCenters.Count > 0)
        {
            Vector3Int closest = ClosestPoint(currentRoom, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector3Int> newPath = new HashSet<Vector3Int>();
            var position = currentRoom;
            newPath.Add(position * 8);
            while (position.z != closest.z)
            {
                if (closest.z > position.z)
                {
                    position += Vector3Int.forward;
                }
                else if (closest.z < position.z)
                {
                    position += Vector3Int.back;
                }
                newPath.Add(position * 8);
            }
            while (position.x != closest.x)
            {
                if (closest.x > position.x)
                {
                    position += Vector3Int.right;
                }
                else if (closest.x < position.x)
                {
                    position += Vector3Int.left;
                }
                newPath.Add(position * 8);
            }

            newPath.ExceptWith(allGrounds);
            GameObject Path = new GameObject("path_" + pathNumber.ToString());
            Path.transform.SetParent(Level.transform, false);

            foreach (var path in newPath)
            {
                GameObject childObject = Instantiate(floorPrefab, path, Quaternion.identity) as GameObject;
                childObject.AddComponent<BoxCollider>();
                childObject.GetComponent<BoxCollider>().center = new Vector3(0, -1, 0);
                //childObject.AddComponent<NavMeshSurface>();
                //surfaces.Add(childObject.GetComponent<NavMeshSurface>());
                childObject.transform.SetParent(Path.transform, false);
            }
            currentRoom = closest;
            paths.UnionWith(newPath);
            pathNumber++;
        }
        allGrounds.UnionWith(paths);

        var wallPositions = FindWallsInDirection(allGrounds, Directions);
        foreach(var wall in wallPositions)
        {
            float offset = Random.Range(0f, 0.01f);
            if (allGrounds.Contains(wall + Directions[0]/2 + Directions[0]/8) == true || 
                allGrounds.Contains(wall + Directions[1]/2 + Directions[1]/8) == true)
            {
                Vector3 actual = (Vector3)wall + (new Vector3(0, offset, 0));
                GameObject childObject = Instantiate(wallPrefab2, actual, wallPrefab2.transform.rotation) as GameObject;
                childObject.transform.SetParent(Level.transform, false);
                childObject.layer = 9;
                childObject.transform.GetChild(0).gameObject.layer = 9;
                childObject.AddComponent<MeshCollider>();
            }
            else
            {
                Vector3 actual = (Vector3)wall + (new Vector3(0, offset, 0));
                GameObject childObject = Instantiate(wallPrefab1, actual, Quaternion.identity) as GameObject;
                childObject.transform.SetParent(Level.transform, false);
                childObject.layer = 9;
                childObject.tag = "Walls";
                childObject.transform.GetChild(0).gameObject.layer = 9;
                childObject.transform.GetChild(0).gameObject.tag = "Walls";
                childObject.AddComponent<MeshCollider>();
            }
        }
        
        spawnPillar(Level);
        Level.AddComponent<NavMeshSurface>();
        Level.GetComponent<NavMeshSurface>().BuildNavMesh();
        spawnMonster(Level);
        spawnCrate(Level);
        //Level.transform.position = (new Vector3(GameStatistics.level, 0, GameStatistics.level)) * 500;
        

        if(GameStatistics.level == 0 || GameStatistics.checkpoint)
        {
            playerSpawn(0, spawnRoom, Level);
            GameStatistics.checkpoint = false;
        }
        else
        {
            spawnPoint(0, spawnRoom, Level);
            //GameObject.Find("Character").transform.position = GameObject.Find("SpawnPoint_rest(Clone)").transform.position;
        }



    }

    private Vector3Int ClosestPoint(Vector3Int currentRoom, List<Vector3Int> roomCenters)
    {
        Vector3Int closest = Vector3Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector3Int.Distance(position, currentRoom);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }


    private HashSet<Vector3Int> FindWallsInDirection(HashSet<Vector3Int> floorPositions, List<Vector3Int> directions)
    {
        HashSet<Vector3Int> wallPositions = new HashSet<Vector3Int>();
        float offset = Random.Range(0f, 0.0000001f);
        foreach(var position in floorPositions)
        {
            foreach(var direction in directions)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition) == false)
                {
                    neighbourPosition = position + direction / 2 + direction / 8;
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }

    public void spawnMonster(GameObject Level)
    {
        int threshold;
        for(int i = 1; i < roomList.Count; i++)
        {
            if (roomList[i].size.x > 8 || roomList[i].size.z > 8)
            {
                threshold = 6;
            }
            else if (roomList[i].size.x > 6 || roomList[i].size.z > 6)
            {
                threshold = 4;
            }
            else
            {
                threshold = 2;
            }

            //GameObject Room = new GameObject("room_" + pathNumber.ToString());
            Vector3Int coordinate;
            while (threshold > 0)
            {
                int rand = 0;
                int xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                int zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                while (allObjects.Contains(coordinate))
                {
                    xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                    zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                    coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                }
                GameObject monster = monsterPrefab[0];
                if (GameStatistics.level < 4)
                {
                    rand = Random.Range(0, 4);
                    monster = monsterPrefab[rand];
                }
                else if (GameStatistics.level < 7)
                {
                    rand = Random.Range(0, 6);
                    monster = monsterPrefab[rand];

                }
                else if (GameStatistics.level <= 10)
                {
                    rand = Random.Range(2, 7);
                    monster = monsterPrefab[rand];
                }

                if(threshold == 1)
                {
                    monster = monsterPrefab[0];
                }
                GameObject childObject = Instantiate(monster, coordinate, Quaternion.identity) as GameObject;
                childObject.transform.rotation = Quaternion.Euler(Facing[Random.Range(0, 4)]);
                childObject.transform.SetParent(Level.transform, false);
                allObjects.Add(coordinate);
                if(rand < 4)
                {
                    threshold--;
                }
                else if(rand < 7)
                {
                    threshold -= 2;
                }
            }
        }
    }

    public void spawnPillar(GameObject Level)
    {
        int threshold;
        for (int i = 1; i < roomList.Count; i++)
        {
            if (roomList[i].size.x > 8 || roomList[i].size.z > 8)
            {
                threshold = 6;
            }
            else if (roomList[i].size.x > 6 || roomList[i].size.z > 6)
            {
                threshold = 4;
            }
            else
            {
                threshold = 2;
            }

            Vector3Int coordinate;
            while (threshold > 0)
            {
                int xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                int zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                while (allObjects.Contains(coordinate))
                {
                    xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                    zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                    coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                }
                GameObject childObject = Instantiate(pillarDecoPrefab, coordinate, Quaternion.identity) as GameObject;
                childObject.transform.SetParent(Level.transform, false);
                childObject.AddComponent<MeshCollider>();
                allObjects.Add(coordinate);
                threshold--;
            }
        }
    }

    public void spawnCrate(GameObject Level)
    {
        int threshold;
        Vector3Int coordinate;
        for (int i = 1; i < roomList.Count; i++)
        {
            if (roomList[i].size.x > 8 || roomList[i].size.z > 8)
            {
                threshold = 6;
            }
            else if (roomList[i].size.x > 6 || roomList[i].size.z > 6)
            {
                threshold = 4;
            }
            else
            {
                threshold = 2;
            }

            while (threshold > 0)
            {
                int xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                int zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                while (allObjects.Contains(coordinate))
                {
                    xCoordinate = Random.Range(roomList[i].xMin + 1, roomList[i].xMax - 1);
                    zCoordinate = Random.Range(roomList[i].zMin + 1, roomList[i].zMax - 1);
                    coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                }
                float check = Random.Range(0f, 1f);
                Vector3 rotation;
                if (check < 0.25)
                {
                    rotation = new Vector3(0, 90, 0);
                }
                else if(check < 0.5)
                {
                    rotation = new Vector3(0, 180, 0);
                }
                else if(check < 0.75)
                {
                    rotation = new Vector3(0, 270, 0);
                }
                else
                {
                    rotation = new Vector3(0, 0, 0);
                }

                if(Random.Range(0f,1f) > 0.5f)
                {
                    GameObject crate = Instantiate(cratePrefab[0], coordinate, Quaternion.identity) as GameObject;
                    crate.transform.SetParent(Level.transform, false);
                }
                else
                {
                    GameObject crate = Instantiate(cratePrefab[1], coordinate, Quaternion.identity) as GameObject;
                    crate.transform.SetParent(Level.transform, false);
                    crate.transform.rotation = Quaternion.Euler(rotation);
                }
                allObjects.Add(coordinate);

                threshold--;
            }
        }
    }

    public void playerSpawn(int roomNumber, BoundsInt room, GameObject Level)
    {
        if(roomNumber == 0)
        {
            float xCoordinate = (room.xMax - room.xMin) / 2;
            float zCoordinate = (room.zMax - room.zMin) / 2;
            Vector3 coordinate = new Vector3(room.xMin + xCoordinate, 0, room.zMin + zCoordinate) * 8;
            Vector3 merchantCoordinate = new Vector3(room.xMin + xCoordinate -1.2f, 0, room.zMin + zCoordinate) * 8;
            Vector3 chestCoordinate = new Vector3(room.xMin + xCoordinate + .5f, 0, room.zMin + zCoordinate) * 8;
            Vector3 puzzleCoordinate = new Vector3(room.xMin + xCoordinate, 0, room.zMin + zCoordinate + 1f) * 8;

            GameObject merchant = Instantiate(merchantPrefab, merchantCoordinate, Quaternion.identity) as GameObject;
            GameObject spawnPoint = Instantiate(spawnPrefab[0], coordinate, Quaternion.identity) as GameObject;
            
            GameObject puzzlePoint = Instantiate(puzzleTeleportPrefab, puzzleCoordinate, Quaternion.identity);
            puzzlePoint.name = "puzzleEntryPoint";
            puzzlePoint.transform.position += new Vector3(0,0.01f,0);
            spawnPoint.transform.SetParent(Level.transform, false);
            merchant.transform.SetParent(Level.transform, false);
            
            puzzlePoint.transform.SetParent(Level.transform, true);
            
            if(GameStatistics.level == 0){
                GameObject chestSpawn = Instantiate(chestPrefab, chestCoordinate, Quaternion.identity) as GameObject;
                chestSpawn.transform.SetParent(Level.transform, false);
                chestSpawn.transform.rotation = Quaternion.Euler(Facing[2]);
            }

        }
    }

    public void spawnPoint(int roomNumber, BoundsInt room, GameObject Level)
    {
        if (roomNumber == 0)
        {
            float xCoordinate = (room.xMax - room.xMin) / 2;
            float zCoordinate = (room.zMax - room.zMin) / 2;
            Vector3 coordinate = new Vector3(room.xMin + xCoordinate, 0, room.zMin + zCoordinate) * 8;
            //Vector3 chestCoordinate = new Vector3(room.xMin + xCoordinate + .5f, 0, room.zMin + zCoordinate) * 8;
            Vector3 merchantCoordinate = new Vector3(room.xMin + xCoordinate-1.2f , 0, room.zMin + zCoordinate) * 8;
            Vector3 puzzleCoordinate = new Vector3(room.xMin + xCoordinate, 0, room.zMin + zCoordinate + 1f) * 8;
            GameObject spawnPoint = Instantiate(spawnPrefab[1], coordinate, Quaternion.identity) as GameObject;
            GameObject merchant = Instantiate(merchantPrefab, merchantCoordinate, Quaternion.identity) as GameObject;
            //GameObject chestSpawn = Instantiate(chestPrefab, chestCoordinate, Quaternion.identity) as GameObject;
            GameObject.Find("Character").transform.position = spawnPoint.transform.position; //(new Vector3(GameStatistics.level, 0, GameStatistics.level)) * 500;
            GameObject puzzlePoint = Instantiate(puzzleTeleportPrefab, puzzleCoordinate, Quaternion.identity);
            puzzlePoint.name = "puzzleEntryPoint";
            puzzlePoint.transform.position += new Vector3(0,0.01f,0);
            puzzlePoint.transform.SetParent(Level.transform, true);
            spawnPoint.transform.SetParent(Level.transform, false);
            merchant.transform.SetParent(Level.transform, false);
            //chestSpawn.transform.SetParent(Level.transform, false);
            //chestSpawn.transform.rotation = Quaternion.Euler(Facing[2]);
        }
    }

    public void proceedPoint(int roomNumber, BoundsInt room, GameObject Level)
    {
        if (roomNumber == roomList.Count - 1)
        {
            float xCoordinate = (room.xMax - room.xMin) / 2;
            float zCoordinate = (room.zMax - room.zMin) / 2;
            Vector3 coordinate = new Vector3(room.xMin + xCoordinate, 0, room.zMin + zCoordinate) * 8;
            allObjects.Add(coordinate);
            GameObject proceedPoint = Instantiate(proceedPrefab, coordinate, Quaternion.identity) as GameObject;
            proceedPoint.name = "Objective";
            proceedPoint.transform.SetParent(Level.transform, false);
        }
    }

    public void destroyLevel()
    {
        if (GameObject.Find("level"))
        {
            GameObject.Find("level").transform.position = new Vector3(0,-100,0);
            Destroy(GameObject.Find("level"));
        }
    }

    public void destroyBossRoom()
    {
        if (GameObject.Find("boss"))
            Destroy(GameObject.Find("boss"));
    }

    public void setMerchant()
    {
        merchantDialogue.SetActive(true);
    }

    public void setMerchantOff()
    {
        merchantDialogue.SetActive(false);
    }

    public void setHint()
    {
        hint.SetActive(true);
    }

    public void setHintOff()
    {
        hint.SetActive(false);
    }


    public void setPuzzleHint()
    {
        puzzleHint.SetActive(true);
    }

    public void setPuzzleHintOff()
    {
        puzzleHint.SetActive(false);
    }



    public void setPuzzleInstruct()
    {
        puzzleInstruct.SetActive(true);
    }

    public void setPuzzleInstructOff()
    {
        puzzleInstruct.SetActive(false);
    }
}
