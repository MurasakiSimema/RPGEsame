using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{
    public string spellName;
    public int spellCost;
    public Text nameText, costText;

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
        if (BattleManager.instance.activeBattler[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.activeBattler[BattleManager.instance.currentTurn].currentMP -= spellCost;
        }
        else
        {
            BattleManager.instance.battleNotification.awakeTime = 1.5f;
            BattleManager.instance.battleNotification.Activate("Not Enought MP!");
            BattleManager.instance.magicMenu.SetActive(false);
        }
    }
}
