using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailabe;
    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, atk, def, wpnAtk, armDef;
    public bool isDead;

    public SpriteRenderer sprite;
    public Sprite deadSpirte, aliveSprite;
    private bool shouldFade;
    public float fadeSpeed = 1f;

    public BattleChar(){
        
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
            sprite.color = new Color(Mathf.MoveTowards(sprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sprite.color.g, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sprite.color.b, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sprite.color.a, 0f, fadeSpeed * Time.deltaTime));
        if (sprite.color.a == 0)
            gameObject.SetActive(false);
    }
    public void EnemyFade()
    {
        shouldFade = true;
    }
    public virtual BattleMove ChooseAttack(BattleMove[] moves){
        int selectAttackIdx = Random.Range(0, movesAvailabe.Length);
        
        return moves.Where(move => move.moveName == this.movesAvailabe[selectAttackIdx]).ToArray()[0];
    }
}
