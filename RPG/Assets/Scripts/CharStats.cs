using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public Sprite charImage;
    public int playerLevel = 1;
    public int maxLevel = 8;
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
        expToNextLevel = new int[8];
        expToNextLevel[0] = 0;
        for (int x = 1; x < maxLevel; x++) 
            expToNextLevel[x] = Mathf.FloorToInt(2000 - (7325 * x) / 3 + (1625 * Mathf.Pow(x, 2)) / 2 + (375 * Mathf.Pow(x, 3)) / 2 - (125 * Mathf.Pow(x, 4)) / 2 + (25 * Mathf.Pow(x, 5)) / 6);
        
        //expToNextLevel = new int[] { 0, 500, 1000, 3000, 5500, 7500, 8500, 9000 };
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


        if (playerLevel < expToNextLevel.Length && playerExp >= expToNextLevel[playerLevel]) 
        {
            playerLevel++;
            if (playerLevel % 2 == 0)
                atk++;
            else
                def++;

            int newhp = Mathf.FloorToInt(maxHP * 0.25f);
            maxHP += newhp;
            CurrentHP = newhp;

            maxMP += 5 * (playerLevel - 1);
            CurrentMP = maxMP;

            AddExp(0);
        }
    }
}
