using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    
    public bool gameMenuOpen, dialogActive, fadingArea;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public List<Item> referenceItem;

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
        if(gameMenuOpen || dialogActive || fadingArea)
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
}
