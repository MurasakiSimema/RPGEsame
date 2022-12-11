using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;

    public Text xpText, itemText;
    public GameObject rewardScreen;

    private string[] rewardItems;
    private int xpEarned;

    public bool markQuestComplete;
    public string questToMark;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenRewardScreen(int xp, string[] rewards)
    {
        rewardItems = rewards;
        xpEarned = xp;

        xpText.text = "Everyone earned: " + xp + " xp";
        itemText.text = "";

        foreach(var item in rewardItems)
        {
            itemText.text += item + "\n";
        }

        rewardScreen.SetActive(true);
    }
    public void CloseRewardScreen()
    {
        foreach (var player in GameManager.instance.playerStats)
            if (player.gameObject.activeInHierarchy)
                player.AddExp(xpEarned);

        foreach (var item in rewardItems)
            GameManager.instance.AddItem(item);

        rewardScreen.SetActive(false);

        GameManager.instance.battleActive = false;

        if (markQuestComplete)
            QuestManager.instance.MarkQuestTrue(questToMark);
    }
}
