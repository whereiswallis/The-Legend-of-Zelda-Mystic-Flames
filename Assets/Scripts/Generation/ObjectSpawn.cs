using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;

    void Start()
    {
        //spawnMonster();
    }
    public void spawnMonster()
    {
        int threshold;
        foreach (var room in Generate.roomList)
        {
            if (room.size.x > 8 || room.size.z > 8)
            {
                threshold = 6;
            }
            else if (room.size.x > 6 || room.size.z > 6)
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
                int xCoordinate = Random.Range(room.xMin + 1, room.xMax - 1);
                int zCoordinate = Random.Range(room.zMin + 1, room.zMax - 1);
                coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                while (Generate.allObjects.Contains(coordinate))
                {
                    xCoordinate = Random.Range(room.xMin + 1, room.xMax - 1);
                    zCoordinate = Random.Range(room.zMin + 1, room.zMax - 1);
                    coordinate = new Vector3Int(xCoordinate, 0, zCoordinate) * 8;
                }
                GameObject childObject = Instantiate(monsterPrefab, coordinate, Quaternion.identity) as GameObject;
                Generate.allObjects.Add(coordinate);
                threshold--;
            }
        }
    }
}
