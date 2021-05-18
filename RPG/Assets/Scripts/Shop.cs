using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public string[] itemToSell;

    public GameObject shopMenu, buyMenu, sellMenu;
    public Text goldText;
    public ItemButton[] buyItemButtons, sellItemButtons;
    
    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold + "g";
        OpenBuyMenu();
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
        OpenBuyMenu();
    }
    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemToSell.Length > i)
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItem(itemToSell[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }
    public void OpenSellMenu()
    {
        buyItemButtons[0].Press();
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        GameManager.instance.SortItems();
        ShowSellItem();
    }
    public void ShowSellItem()
    {
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItem(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectBuyItem(Item item)
    {
        selectedItem = item;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }
    public void SelectSellItem(Item item)
    {
        selectedItem = item;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * 0.6f) + "g";
    }
    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);
            }
            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }
    public void SellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * 0.6f);
            GameManager.instance.RemoveItem(selectedItem.itemName);

            ShowSellItem();
            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }
}
