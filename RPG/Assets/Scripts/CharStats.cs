using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public Sprite charImage;
    public int playerLevel = 1;
    public int maxLevel = 20;
    public int playerExp = 0;
    public int[] expToNextLevel;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 50;
    public int atk = 1;
    public int def = 1;
    public int weaponAtk;
    public string weaponName;
    public int armorDef;
    public string armorName;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[] { 0, 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000, 100000, 120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 };
    }
    public int CurrentHP
    {
        get => currentHP;
        set
        {
            currentHP += value;
            if (currentHP >= maxHP)
                currentHP = maxHP;
        }
    }
    public int CurrentMP
    {
        get => currentMP;
        set
        {
            currentMP += value;
            if (currentMP >= maxMP)
                currentMP = maxMP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(expToNextLevel[Mathf.Min(playerLevel, 19)]);
        }
    }

    public void AddExp(int exp)
    {
        playerExp += exp;


        if (playerLevel < expToNextLevel.Length && playerExp > expToNextLevel[playerLevel]) 
        {
            playerLevel++;
            if (playerLevel % 2 == 0)
                atk++;
            else
                def++;

            int newhp = Mathf.FloorToInt(maxHP * 0.25f);
            maxHP += newhp;
            currentHP = newhp;

            maxMP += 5 * (playerLevel - 1);
            currentMP += maxMP;
        }
    }
}
