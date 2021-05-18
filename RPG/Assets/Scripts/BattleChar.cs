using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailabe;

    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, atk, def, wpnAtk, armDef;
    public bool isDead;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
