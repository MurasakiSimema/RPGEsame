using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject menu;
    public GameObject[] windows;

    public Text[] nameText, hpText, mpText, lvlText, expTotText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatsHolder;
    public GameObject[] statusButtons;

    private CharStats[] playerStats;

    public Text statusName, statusHP, statusMP, statusAtk, statusDef, statusWpnName, statusWpnAtk, statusArmName, statusArmDef, statusExp;
    public Image statusImage;

    [Header("Items")]
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItems;
    public Text itemName, itemDescription, useButtonText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (menu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                menu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expTotText[i].text = "Exp: " + playerStats[i].playerExp;
                expText[i].text = playerStats[i].playerExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].playerExp;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
                charStatsHolder[i].SetActive(false);
        }
    }

    public void ToggleWindow(int n)
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == n)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        foreach (GameObject win in windows)
            win.SetActive(false);

        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        StatusChar(0);
        for(int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }
    public void StatusChar(int n)
    {
        statusName.text = playerStats[n].charName;
        statusHP.text = playerStats[n].currentHP + "/" + playerStats[n].maxHP;
        statusMP.text = playerStats[n].currentMP + "/" + playerStats[n].maxMP;
        statusAtk.text = playerStats[n].atk.ToString();
        statusDef.text = playerStats[n].def.ToString();

        if (playerStats[n].weaponName != "")
            statusArmName.text = playerStats[n].weaponName;
        else
            statusArmName.text = "None";
        statusWpnAtk.text = playerStats[n].weaponAtk.ToString();

        if (playerStats[n].armorName != "")
            statusArmName.text = playerStats[n].armorName;
        else
            statusArmName.text = "None";
        statusArmDef.text = playerStats[n].armorDef.ToString();

        statusExp.text = (playerStats[n].expToNextLevel[playerStats[n].playerLevel] - playerStats[n].playerExp).ToString();

        statusImage.sprite = playerStats[n].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItem(GameManager.instance.itemHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectedItem(Item item)
    {
        activeItems = item;

        if (activeItems.isItem)
            useButtonText.text = "Use";
        else if (activeItems.isWeapon || activeItems.isArmor)
            useButtonText.text = "Equip";

        itemName.text = activeItems.itemName;
        itemDescription.text = activeItems.description;
    }
}
