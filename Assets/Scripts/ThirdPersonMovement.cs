using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public UpgradeMenu upgradeMenu;
    public CharacterController controller;
    
    public float baseSpeed;
    public float speed = 8;
    public float rotation_speed = 1000f;



    private void Start() 
    {
        upgradeMenu = GameObject.Find("Upgrade Menu").GetComponent<UpgradeMenu>();
        GameObject.Find("Upgrade Menu").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //speed = baseSpeed + (0.1f*upgradeMenu.speedLevel*baseSpeed);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 ,gameObject.transform.position.z);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        

        
        Vector3 direction = new Vector3(horizontal, -(Mathf.Abs(0.01f*(horizontal+vertical))), vertical).normalized;
        if (direction.magnitude >= 0.1f){
            if (gameObject.transform.position.y < -0.1f || gameObject.transform.position.y > 0.1f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 ,gameObject.transform.position.z);
            }
            else
                controller.Move(direction* (speed + GameStatistics.speedLevel)* Time.deltaTime);
        }

        

    }


}
