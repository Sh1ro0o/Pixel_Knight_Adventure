using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] Button attackUpgradeButton, healthUpgradeButton;
    [SerializeField] TextMeshProUGUI attackUpgradeCostText, healthUpgradeCostText, yourCoinsAmmountText, currentAttackText, currentHealthText;

    int upgradeLevelAttack, upgradeLevelHealth;
    int playerHealth, playerAttack;

    UpgradeLevelCost attackUpgradeLevelCost, healthUpgradeLevelCost;

    PlayerCombat player;

    enum UpgradeLevelCost
    {
        upgradeLevel0 = 3,
        upgradeLevel1 = 5,
        upgradeLevel2 = 10,
        max = -1
    }

    // Start is called before the first frame update
    void Start()
    {
        //gets upgrade level
        upgradeLevelAttack = PlayerPrefs.GetInt("UpgradeLevelAttack", 0);
        upgradeLevelHealth = PlayerPrefs.GetInt("UpgradeLevelHealth", 0);

        //displays how many coins the player has
        yourCoinsAmmountText.text = GameManager.gameManager.totalCoins + "x";

        //displays upgrade requirements and sets upgrade level
        upgradeAttackRequirementsUpdate();
        upgradeHealthRequirementsUpdate();

        //displays players current attack and health
        currentAttackText.text = playerAttack + "";
        currentHealthText.text = playerHealth + "";
    }

    // Update is called once per frame
    void Update()
    {
        //check if totalCoins match the required ammount so you can enable/disable the buttons

        //attack button
        if (GameManager.gameManager.totalCoins >= (int)attackUpgradeLevelCost)
            attackUpgradeButton.interactable = true;
        else
            attackUpgradeButton.interactable = false;

        //health button
        if (GameManager.gameManager.totalCoins >= (int)healthUpgradeLevelCost)
            healthUpgradeButton.interactable = true;
        else
            healthUpgradeButton.interactable = false;

        //dissable upgrade buttons at max level
        if (upgradeLevelHealth == -1)
            healthUpgradeButton.interactable = false;

        if (upgradeLevelAttack == -1)
            attackUpgradeButton.interactable = false;

    }

    void upgradeAttackRequirementsUpdate()
    {
        switch(upgradeLevelAttack)
        {
            case 0:
                attackUpgradeLevelCost = UpgradeLevelCost.upgradeLevel0;
                attackUpgradeCostText.text = (int)attackUpgradeLevelCost + "x";
                playerAttack = 10;
                break;
            case 1:
                attackUpgradeLevelCost = UpgradeLevelCost.upgradeLevel1;
                attackUpgradeCostText.text = (int)attackUpgradeLevelCost + "x";
                playerAttack = 20;
                break;
            case 2:
                attackUpgradeLevelCost = UpgradeLevelCost.upgradeLevel2; 
                attackUpgradeCostText.text = (int)attackUpgradeLevelCost + "x";
                playerAttack = 30;
                break;
            case -1:
                attackUpgradeLevelCost = UpgradeLevelCost.max;
                attackUpgradeCostText.text = "MAX";
                playerAttack = 40;
                break;
            default: attackUpgradeCostText.text = "Error";
                break;
        }
    }

    void upgradeHealthRequirementsUpdate()
    {
        switch (upgradeLevelHealth)
        {
            case 0:
                healthUpgradeLevelCost = UpgradeLevelCost.upgradeLevel0;
                healthUpgradeCostText.text = (int)healthUpgradeLevelCost + "x";
                playerHealth = 100;
                break;
            case 1:
                healthUpgradeLevelCost = UpgradeLevelCost.upgradeLevel1;
                healthUpgradeCostText.text = (int)healthUpgradeLevelCost + "x";
                playerHealth = 125;
                break;
            case 2:
                healthUpgradeLevelCost = UpgradeLevelCost.upgradeLevel2;
                healthUpgradeCostText.text = (int)healthUpgradeLevelCost + "x";
                playerHealth = 150;
                break;
            case -1:
                healthUpgradeLevelCost = UpgradeLevelCost.max;
                healthUpgradeCostText.text = "MAX";
                playerHealth = 200;
                break;
            default: healthUpgradeCostText.text = "Error";
                break;
        }
    }

    public void HealthUpgradeButton ()
    {
        //take coins and update text
        GameManager.gameManager.RemoveCoins((int)healthUpgradeLevelCost);
        yourCoinsAmmountText.text = GameManager.gameManager.totalCoins + "x";

        //upgrade health
        if (upgradeLevelHealth < 2)
            upgradeLevelHealth++; //increase level
        else
            upgradeLevelHealth = -1; //set to max level

        //display new coins ammount to upgrade and new health ammount
        upgradeHealthRequirementsUpdate();
        currentHealthText.text = playerHealth + "";

        //save player max health upgrade
        PlayerPrefs.SetInt("UpgradeLevelHealth", upgradeLevelHealth);
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);

        Debug.Log("Player maxhealth increased to: " + PlayerPrefs.GetInt("PlayerHealth"));
    }

    void ResetStats()
    {
        PlayerPrefs.SetInt("UpgradeLevelHealth", 0);
        PlayerPrefs.SetInt("PlayerHealth", 100);
        PlayerPrefs.SetInt("PlayerAttack", 10);
    }
}
