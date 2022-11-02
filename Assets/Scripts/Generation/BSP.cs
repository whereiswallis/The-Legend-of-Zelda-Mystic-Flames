using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BSP : MonoBehaviour
{
    public static List<BoundsInt> partition(BoundsInt space, int minWidth, int minLength)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(space);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.z >= minLength && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.z >= minLength * 2)
                    {
                        HorizontalSplit(roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        VerticalSplit(roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth && room.size.z >= minLength)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        VerticalSplit(roomsQueue, room);
                    }
                    else if (room.size.z >= minLength * 2)
                    {
                        HorizontalSplit(roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.z >= minLength)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void VerticalSplit(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void HorizontalSplit(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var zSplit = Random.Range(1, room.size.z);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, room.size.y, zSplit));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y, room.min.z + zSplit),
            new Vector3Int(room.size.x, room.size.y, room.size.z - zSplit));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
