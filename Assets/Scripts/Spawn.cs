using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject player;

    private void Awake()     
    {
        player = Instantiate(player, transform.position, transform.rotation);
        player.name = "Character";
    }

}
