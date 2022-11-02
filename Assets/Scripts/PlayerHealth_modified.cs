using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth_modified : MonoBehaviour
{
    public int player_max_health = 25;
    public int player_health;
    public HealthBar player_healthbar;
    public GameObject gameOverScreen;
    private float deadTimer;
    private bool isDead = false;
    private bool isHit = false;
    private float hitTimer = 0;
    private float flinchTime = .25f;

    private bool isInvincible = false;
    private float invincibleTimer = 0;
    private float invincibleTime = 1.25f; // including the flinchTime


    private bool godMode = false;



    Shader normalShader;
    Shader hitShader;
    //Renderer rend;

    //public List<Collider> colliders;


    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.Find("Game Over");
        gameOverScreen.SetActive(false);
        player_max_health = 25 + 5* GameStatistics.healthLevel;
        player_health = player_max_health;

        player_healthbar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        player_healthbar.SetMaxHealth(player_max_health);
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        
        //normalShader = Shader.Find("Unlit/AlwaysVisible");
        normalShader = Shader.Find("Standard");
        hitShader = Shader.Find("Unlit/FadeShader");

        /*
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            colliders.Add(collider);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {


        if (godMode)
        {
            player_health = player_max_health;
            player_healthbar.setHealth(player_health);
        }

        /*
        if(Input.GetKeyDown(KeyCode.G)){
            godMode = true;
            Debug.Log("GodModeOn");

        }


        if(Input.GetKeyDown(KeyCode.H)){
            godMode = false;
            Debug.Log("GodModeOff");
        }
        */




        //if(Input.GetKeyDown(KeyCode.P)){
        //    TakeDamage(20);
        //}


        if ((Time.time > deadTimer)&& isDead)
        {

            //animator.enabled = false;
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.shader = normalShader;
            }
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                gameOverScreen.GetComponent<GameOverScreen>().Invoke("GameOver", 1.0f);
            }
            this.enabled = false;
        }


        if (Time.time > invincibleTimer && isInvincible){
            isInvincible = false;
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.shader = normalShader;
            }GetComponent<Collider>().enabled = true;
        }



        if ((Time.time > hitTimer) && isHit && !isDead)
        {
            isHit = false;
            GetComponent<PlayerAnimationsModified>().enabled = true;
            GetComponent<Player_Rotation>().enabled = true;
            GetComponentInParent<ThirdPersonMovement>().enabled = true;
        }
        
        //if(player_health <= 0){
        //   Respawn();
        //}
    }

    public void TakeDamage(int damage)
    {   
        if (! GetComponent<PlayerAnimationsModified>().isEvading && !isDead && !isInvincible)
        {
        player_health -= (int)(damage * (1 - (0.1f * GameStatistics.defenceLevel)));
        if (GameStatistics.level != 5 || GameStatistics.level != 10){
            player_health -= (int)(GameStatistics.level / 2);
        }
        player_healthbar.setHealth(player_health);
        animator.SetTrigger("isHit");
        isHit = true;
        isInvincible = true;
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            rend.material.shader = hitShader;
        }
        
        GetComponent<PlayerAnimationsModified>().enabled = false;
        GetComponent<Player_Rotation>().enabled = false;
        GetComponentInParent<ThirdPersonMovement>().enabled = false;
        GetComponent<Collider>().enabled = false;
        hitTimer = Time.time + flinchTime;
        invincibleTimer = Time.time + invincibleTime;
        Debug.Log("Player is hit");
        if(player_health <= 0){
            die();
        }
        }
    }

    public void TakeDefendDamage(int damage)
    {
        if (! GetComponent<PlayerAnimationsModified>().isEvading && ! isDead && !isInvincible)
        {
        player_health -= (int) (damage * (1 - (0.1f * GameStatistics.defenceLevel)));
        player_healthbar.setHealth(player_health);
        animator.SetTrigger("defendingHit");
        
        Debug.Log("Player is hit when defending");
        if(player_health <= 0){
            die();
            
        }
        }
    }

    public void PotionHeal()
    {
        player_health += (int)(player_max_health * 0.1f);
        if(player_health > player_max_health)
        {
            player_health = player_max_health;
        }
        player_healthbar.setHealth(player_health);
    }

    void die()
    {
        animator.SetBool("isDead",true);
        isDead = true;
        GetComponent<PlayerAnimationsModified>().enabled = false;
        GetComponent<Player_Rotation>().enabled = false;
        GetComponentInParent<ThirdPersonMovement>().enabled = false;
        deadTimer = Time.time + .5f;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            LevelChanger.levelToLoad = 2;
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
        }
        if(GameObject.Find("Ability 1").GetComponent<Ability_1>().ability1_active){
            GameObject.Find("Ability 1").GetComponent<Ability_1>().ability1_active = false;
            GameObject.Find("Ability 1").GetComponent<Ability_1>().EndAbility1();
        }
        if(GameObject.Find("Ability 2").GetComponent<Ability_2>().ability2_active){
            GameObject.Find("Ability 2").GetComponent<Ability_2>().ability2_active = false;
            GameObject.Find("Ability 2").GetComponent<Ability_2>().EndAbility2();
        }
        
        //animator.enabled = false;


    }



    public void IncreaseMaxHealth(int maxHealth){
        this.player_max_health += maxHealth;
        player_health = player_max_health;
    }

    public int getMaxHealth(){
        return this.player_max_health;
    }

    public void setInvincible(bool whatever)
    {
        this.isInvincible = whatever;
    }

    //void Respawn(){
    //    gameOverScreen.GameOver();
    //}
}
