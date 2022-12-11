using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleType 
{
    public int exp;
    public string[] enemies;
    public string[] items;
    public bool denyEscape;
    public int audioToPlay = 0;
}
