using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Press()
    {
        if (GameMenu.instance.menu.activeInHierarchy)
        {
            if (GameManager.instance.itemsHeld[buttonValue] != "")
            {
                GameMenu.instance.SelectedItem(GameManager.instance.GetItem(GameManager.instance.itemsHeld[buttonValue]));
            }
        }else if(Shop.instance.shopMenu.activeInHierarchy){
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(GameManager.instance.GetItem(Shop.instance.itemToSell[buttonValue]));
            }else if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(GameManager.instance.GetItem(GameManager.instance.itemsHeld[buttonValue]));
            }
        }else if (GameManager.instance.battleActive)
        {
            if (GameManager.instance.itemsHeld[buttonValue] != "")
            {
                BattleManager.instance.SelectItem(GameManager.instance.GetItem(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
    }
}
