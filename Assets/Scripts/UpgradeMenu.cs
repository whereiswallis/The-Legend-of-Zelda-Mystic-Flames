using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Drawing;

public class UpgradeMenu : MonoBehaviour
{
    public List<int> upgradeCosts = new List<int>() {50, 100, 300, 1000, 5000};
    public Money money;
    public RectTransform healthBar;
    public RectTransform healthHeart;
    
    //public PlayerHealth_modified player;

    public GameObject player;
    public HealthBar player_healthbar;

    public int healthLevel;
    public int attackLevel;
    public int defenceLevel;
    public int speedLevel;
    public int coinLevel;

    public Image healthImage;
    public Image attackImage;
    public Image defenceImage;
    public Image speedImage;
    public Image coinImage;

    public Text healthCost;
    public Text attackCost;
    public Text defenceCost;
    public Text speedCost;
    public Text coinCost;

    public Sprite noBars;
    public Sprite oneBar;
    public Sprite twoBar;
    public Sprite threeBar;
    public Sprite fourBar;
    public Sprite fullBar;

    
    // Start is called before the first frame update
    void Start()
    {
        healthLevel = 0;
        attackLevel = 0;
        defenceLevel = 0; 
        speedLevel = 0; 
        coinLevel = 0;

        player = GameObject.Find("Character");

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Character");
    }

    public void EnterUpgradeManu(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void upgradeHealth(){
        string cost;
        List<Sprite> bars = new List<Sprite>() {
            noBars, oneBar, twoBar, threeBar, fourBar, fullBar
        };
        if ((healthLevel < 5) && (upgradeCosts[healthLevel] <= money.money)){
            if (healthLevel + 1 == 5) {
                cost = "-";
            }
            else {
                cost = upgradeCosts[healthLevel+1].ToString();
            }
            updateMenu(bars[healthLevel+1], healthImage, healthCost, cost);
            money.Spend(upgradeCosts[healthLevel]);
            healthLevel += 1;
            GameStatistics.healthLevel++;

            // Update health values of player


            player.GetComponentInChildren<PlayerHealth_modified>().IncreaseMaxHealth(5);
            player_healthbar.SetMaxHealth(player.GetComponentInChildren<PlayerHealth_modified>().getMaxHealth());
            //player.player_max_health += 20;
            //player.player_health = player.player_max_health;
            //player_healthbar.SetMaxHealth(player.player_max_health);

            // Fix the UI to reflect health change

            float perInc = ((healthBar.localScale.x + 0.3f) - healthBar.localScale.x)/healthBar.localScale.x;
            float xTemp = healthBar.localScale.x + (perInc*healthBar.localScale.x);
            float tempXPos = healthBar.localPosition.x;
            healthBar.localScale = new Vector3(xTemp, healthBar.localScale.y,healthBar.localScale.z);
            xTemp = tempXPos + (perInc/2f)*healthBar.rect.width*(healthBar.localScale.x - 0.3f);
            healthBar.localPosition = new Vector3(xTemp, healthBar.localPosition.y, healthBar.localPosition.z);
            xTemp = healthHeart.localScale.x/(1f+perInc);
            healthHeart.localScale = new Vector3(xTemp, healthHeart.localScale.y, healthHeart.localScale.z);
        }
    }

    public void upgradeAttack(){
        string cost;
        List<Sprite> bars = new List<Sprite>() {
            noBars, oneBar, twoBar, threeBar, fourBar, fullBar
        };
        if ((attackLevel < 5) && (upgradeCosts[attackLevel] <= money.money)){
            if (attackLevel + 1 == 5) {
                cost = "-";
            }
            else {
                cost = upgradeCosts[attackLevel+1].ToString();
            }
            updateMenu(bars[attackLevel+1], attackImage, attackCost, cost);
            money.Spend(upgradeCosts[attackLevel]);
            attackLevel += 1;
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack += 5;
            GameStatistics.attackLevel++;
        }
    }

    public void upgradeDefence(){
        string cost;
        List<Sprite> bars = new List<Sprite>() {
            noBars, oneBar, twoBar, threeBar, fourBar, fullBar
        };
        if ((defenceLevel < 5) && (upgradeCosts[defenceLevel] <= money.money)){
            if (defenceLevel + 1 == 5) {
                cost = "-";
            }
            else {
                cost = upgradeCosts[defenceLevel+1].ToString();
            }
            updateMenu(bars[defenceLevel+1], defenceImage, defenceCost, cost);
            money.Spend(upgradeCosts[defenceLevel]);
            defenceLevel += 1;
            GameStatistics.defenceLevel++;
        }
    }

    public void upgradeSpeed(){
        string cost;
        List<Sprite> bars = new List<Sprite>() {
            noBars, oneBar, twoBar, threeBar, fourBar, fullBar
        };
        if ((speedLevel < 5) && (upgradeCosts[speedLevel] <= money.money)){
            if (speedLevel + 1 == 5) {
                cost = "-";
            }
            else {
                cost = upgradeCosts[speedLevel+1].ToString();
            }
            updateMenu(bars[speedLevel+1], speedImage, speedCost, cost);
            money.Spend(upgradeCosts[speedLevel]);
            speedLevel += 1;
            GameStatistics.speedLevel++;
        }
    }

    public void upgradeCoin(){
        string cost;
        List<Sprite> bars = new List<Sprite>() {
            noBars, oneBar, twoBar, threeBar, fourBar, fullBar
        };
        if ((coinLevel < 5) && (upgradeCosts[coinLevel] <= money.money)){
            if (coinLevel + 1 == 5) {
                cost = "-";
            }
            else {
                cost = upgradeCosts[coinLevel+1].ToString();
            }
            updateMenu(bars[coinLevel+1], coinImage, coinCost, cost);
            money.Spend(upgradeCosts[coinLevel]);
            coinLevel += 1;
            GameStatistics.goldLevel++;
        }
    }

    public void updateMenu(Sprite newBars, Image statBarsLoc, Text costLoc, string newCost){
        statBarsLoc.sprite = newBars;
        costLoc.text = newCost;
    }

}
