using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] battles;
    private bool inArea;

    public bool activateOnEnter, activateOnStay, activateOnExit;

    public float timeBetweenBattle = 10f;
    private float betweenBattleCounter;
    public bool deactivateAfterStart;

    public bool shouldCompleteQuest;
    public string questToComplete;

    // Start is called before the first frame update
    void Start()
    {
        betweenBattleCounter = Random.Range(timeBetweenBattle * 0.5f, timeBetweenBattle * 1.5f);   
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && PlayerController.instance.canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                betweenBattleCounter -= Time.deltaTime;
            }

            if (betweenBattleCounter <= 0)
            {
                betweenBattleCounter = Random.Range(timeBetweenBattle * 0.5f, timeBetweenBattle * 1.5f);

                StartCoroutine(StartBattleCo());
            }
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (activateOnEnter)
                StartCoroutine(StartBattleCo());
            else
                inArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (activateOnExit)
                StartCoroutine(StartBattleCo());
            else
                inArea = false;
        }
    }
    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;

        int selectedBattle = Random.Range(0, battles.Length);

        BattleManager.instance.rewardItems = battles[selectedBattle].items;
        BattleManager.instance.rewardXP = battles[selectedBattle].exp;
        BattleManager.instance.denyEscape = battles[selectedBattle].denyEscape;
        BattleManager.instance.audioToPlay = battles[selectedBattle].audioToPlay;

        yield return new WaitForSeconds(1.5f);
        BattleManager.instance.BattleStart(battles[selectedBattle].enemies);
        UIFade.instance.FadeFromBlack();

        if (deactivateAfterStart)
            gameObject.SetActive(false);

        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = questToComplete;
    }
}
