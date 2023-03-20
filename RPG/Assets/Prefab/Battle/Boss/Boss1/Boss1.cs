using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss1 : BattleChar
{
    private int turn = 0;

    public Boss1() : base(){

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override BattleMove ChooseAttack(BattleMove[] moves)
    {
        turn++;
        if (turn % 3 == 2)
            return moves.Where(move => move.moveName == "Flame").ToArray()[0];
        else
            return base.ChooseAttack(moves);
    }
}
