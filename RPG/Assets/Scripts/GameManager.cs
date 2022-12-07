using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    
    public bool gameMenuOpen, dialogActive, fadingArea, shopActive, battleActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public List<Item> referenceItem;

    public int currentGold;


    public int MaxLv
    {
        get
        {
            int[] livelli = playerStats.Select(player => player.playerLevel).ToArray();
            return Mathf.Max(livelli);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingArea || shopActive|| battleActive) 
            PlayerController.instance.canMove = false;
        else
            PlayerController.instance.canMove = true;

        //if (Input.GetKeyDown(KeyCode.J))
            //AddItem("Iron Armor");
    }

    public Item GetItem(string itemToGrab)
    {
        return referenceItem.Find(item => item.itemName == itemToGrab); ;
    }

    public void SortItems()
    {
        bool itemAfterSpace;

        do
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;
                    if (itemsHeld[i] != "")
                        itemAfterSpace = true;
                }
            }
        } while (itemAfterSpace);
    }

    public void AddItem(string item)
    {
        bool found = false;
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i]=="" || itemsHeld[i] == item)
            {
                foreach(Item elem in referenceItem)
                {
                    if(elem.itemName == item)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    itemsHeld[i] = item;
                    numberOfItems[i]++;
                }
                else
                    Debug.LogError(item + " non trovato");

                break;
            }
        }
        GameMenu.instance.ShowItems();
    }
    public void RemoveItem(string item)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == item)
            {
                numberOfItems[i]--;
                if (numberOfItems[i] <= 0)
                    itemsHeld[i] = "";

                break;
            }
        }
        GameMenu.instance.ShowItems();
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", PlayerController.instance.transform.position.z);

        //charater info
        foreach(var playerStat in playerStats)
        {
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_Active", Convert.ToInt32(playerStat.gameObject.activeInHierarchy));
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_Level", playerStat.playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_Exp", playerStat.playerExp);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_CurrentHP", playerStat.currentHP);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_MaxHP", playerStat.maxHP);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_CurrentMP", playerStat.currentMP);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_MaxMP", playerStat.maxMP);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_Atk", playerStat.atk);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_Def", playerStat.def);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_WeaponAtk", playerStat.weaponAtk);
            PlayerPrefs.SetInt("Player_" + playerStat.charName + "_ArmorDef", playerStat.armorDef);
            PlayerPrefs.SetString("Player_" + playerStat.charName + "_EquippedWeapon", playerStat.weaponName);
            PlayerPrefs.SetString("Player_" + playerStat.charName + "_EquippedArmor", playerStat.armorName);
        }

        //inventory
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemInInventoryAmount_" + i, numberOfItems[i]);
        }
    }
    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_X"), PlayerPrefs.GetFloat("Player_Position_Y"), PlayerPrefs.GetFloat("Player_Position_Z"));

        for (int i = 0; i < playerStats.Length; i++) 
        {
            playerStats[i].gameObject.SetActive(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Active") > 0);

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level");
            playerStats[i].playerExp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Exp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxMP");
            playerStats[i].atk = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Atk");
            playerStats[i].def = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Def");
            playerStats[i].weaponAtk = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WeaponAtk");
            playerStats[i].armorDef = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmorDef");
            playerStats[i].weaponName = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedWeapon");
            playerStats[i].armorName = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedArmor");
        }

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemInInventoryAmount_" + i);
        }
    }
}
