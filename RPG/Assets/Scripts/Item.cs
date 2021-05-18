using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Equip Details")]
    public int weaponAtk, armorDef;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(int charToUse)
    {
        CharStats selectedchar = GameManager.instance.playerStats[charToUse];

        if (isItem)
        {
            if (affectHP)
                selectedchar.CurrentHP += amountToChange;

            if (affectMP)
                selectedchar.CurrentMP += amountToChange;

            if (affectStr)
                selectedchar.atk += amountToChange;
        }
        else if (isWeapon)
        {
            if (selectedchar.weaponName != "")
                GameManager.instance.AddItem(selectedchar.weaponName);
            
            selectedchar.weaponName = itemName;
            selectedchar.weaponAtk = weaponAtk;
        }
        else if (isArmor)
        {
            if (selectedchar.armorName != "")
                GameManager.instance.AddItem(selectedchar.armorName);

            selectedchar.armorName = itemName;
            selectedchar.armorDef = armorDef;
        }

        GameManager.instance.RemoveItem(itemName);
    }
    public void UseBattle(int charToUse)
    {
        BattleChar selectedchar = BattleManager.instance.activeBattler[charToUse];

        if (isItem)
        {
            if (affectHP)
                selectedchar.CurrentHP += amountToChange;

            if (affectMP)
                selectedchar.CurrentMP += amountToChange;

            if (affectStr)
                selectedchar.atk += amountToChange;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
