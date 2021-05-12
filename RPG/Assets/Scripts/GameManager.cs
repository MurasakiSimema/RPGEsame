using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;
    
    public bool gameMenuOpen, dialogActive, fadingArea;

    public string[] itemHeld;
    public int[] numberOfItems;
    public List<Item> referenceItem;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogActive || fadingArea)
            PlayerController.instance.canMove = false;
        else
            PlayerController.instance.canMove = true;
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
            for (int i = 0; i < itemHeld.Length - 1; i++)
            {
                if (itemHeld[i] == "")
                {
                    itemHeld[i] = itemHeld[i + 1];
                    itemHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;
                    if (itemHeld[i] != "")
                        itemAfterSpace = true;
                }
            }
        } while (itemAfterSpace);
    }
}
