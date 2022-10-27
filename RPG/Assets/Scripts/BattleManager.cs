using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene, statsMenu;
    public Transform[] playerPosition, enemiesPosition;
    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattler;

    public int currentTurn;
    public bool turnWaiting;
    public BattleMove[] moves;
    public GameObject enemyAttackEffect;

    public GameObject uiButtonsHolder;
    public DamageNumber damageNumber;

    public Text[] playerNames, playerHP, playerMP;
    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;

    public BattleNotification battleNotification;

    public int chanceToFlee = 25;
    private bool flee;

    public GameObject itemsMenu;
    public ItemButton[] itemButtons;
    public Item selectedItem;
    public Text itemName, itemDescription;

    public string gameOverScene;

    public int rewardXP;
    public string[] rewardItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (battleActive)
        {
            if (turnWaiting)
            {
                if (activeBattler[currentTurn].isPlayer)
                {
                    if (!uiButtonsHolder.gameObject.activeInHierarchy)
                        uiButtonsHolder.SetActive(true);
                }
                else if (!activeBattler[currentTurn].isPlayer)
                {
                    if (uiButtonsHolder.gameObject.activeInHierarchy)
                        uiButtonsHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCo());
                }
            }
        }
    }
    public void BattleStart(string[] enemies)
    {
        if (!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);
            statsMenu.SetActive(true);

            for (int i = 0; i < playerPosition.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for (int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if (playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            var newPlayer = Instantiate(playerPrefabs[j], playerPosition[i].position, playerPosition[i].rotation);
                            newPlayer.transform.parent = playerPosition[i];

                            activeBattler.Add(newPlayer);

                            var player = GameManager.instance.playerStats[i];
                            activeBattler[i].currentHP = player.currentHP;
                            activeBattler[i].maxHP = player.maxHP;
                            activeBattler[i].currentMP = player.currentMP;
                            activeBattler[i].maxMP = player.maxMP;
                            activeBattler[i].atk = player.atk;
                            activeBattler[i].def = player.def;
                            activeBattler[i].wpnAtk = player.weaponAtk;
                            activeBattler[i].armDef = player.armorDef;

                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemies[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemiesPosition[i].position, enemiesPosition[i].rotation);
                            newEnemy.transform.parent = enemiesPosition[i];

                            activeBattler.Add(newEnemy);

                            break;
                        }
                    }
                }
            }

            UpdateUIStats();
            turnWaiting = true;
            currentTurn = 0;
        }
    }
    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeBattler.Count)
            currentTurn = 0;


        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true, allPlayerDead = true;

        foreach (var battler in activeBattler)
        {
            if (battler.currentHP < 0)
                battler.currentHP = 0;

            if (battler.currentHP == 0)
            {
                if (!battler.isDead)
                {
                    battler.isDead = true;
                    if (battler.isPlayer)
                    {
                        battler.sprite.sprite = battler.deadSpirte;
                        battleNotification.awakeTime = 1.5f;
                        battleNotification.Activate(battler.charName + " fainted");
                    }
                    else
                        battler.EnemyFade();
                }
            }
            else
            {
                if (battler.isPlayer)
                {
                    allPlayerDead = false;
                    battler.sprite.sprite = battler.aliveSprite;
                }
                else
                    allEnemiesDead = false;
            }
        }

        if (allPlayerDead)
        {
            StartCoroutine(GameOverCo());
        }
        else if (allEnemiesDead)
        {
            flee = false;
            StartCoroutine(EndBattleCo());
        }
        while (activeBattler[currentTurn].currentHP == 0)
        {
            currentTurn++;
            if (currentTurn >= activeBattler.Count)
                currentTurn = 0;
        }

    }
    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(2f);
        NextTurn();
    }
    public void EnemyAttack()
    {
        var playeridxs = activeBattler.Select((item, index) => new { idx = index, item = item }).Where(battler => battler.item.isPlayer && !battler.item.isDead).ToArray();

        int targetidx = playeridxs[Random.Range(0, playeridxs.Length)].idx;

        var selectAttack = activeBattler[currentTurn].ChooseAttack(moves);

        Instantiate(selectAttack.effect, activeBattler[targetidx].transform.position, activeBattler[targetidx].transform.rotation);
        Instantiate(enemyAttackEffect, activeBattler[currentTurn].transform.position, activeBattler[currentTurn].transform.rotation);
        DealDamage(targetidx, selectAttack.movePower);
    }
    public void DealDamage(int targetidx, int movePwr)
    {
        float atkPwr = activeBattler[currentTurn].atk + activeBattler[currentTurn].wpnAtk;
        float defPwr = activeBattler[targetidx].def + activeBattler[targetidx].armDef;
        float dmg = (atkPwr / defPwr) * movePwr * Random.Range(0.9f, 1.1f);

        activeBattler[targetidx].currentHP -= Mathf.RoundToInt(dmg);
        Debug.Log(activeBattler[currentTurn].charName + " is dealing " + dmg + " to " + activeBattler[targetidx].charName);

        Instantiate(damageNumber, activeBattler[targetidx].transform.position, activeBattler[targetidx].transform.rotation).SetDamage(Mathf.RoundToInt(dmg));
    }
    public void UpdateUIStats()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            if (activeBattler.Count > i && activeBattler[i].isPlayer)
            {
                BattleChar playerData = activeBattler[i];

                playerNames[i].gameObject.SetActive(true);
                playerNames[i].text = playerData.charName;
                playerHP[i].text = playerData.currentHP + "/" + playerData.maxHP;
                playerMP[i].text = playerData.currentMP + "/" + playerData.maxMP;
            }
            else
                playerNames[i].gameObject.SetActive(false);
        }
    }

    public void PlayerAttack(string moveName, int targetidx)
    {
        var selectAttack = moves.Where(move => move.moveName == moveName).ToArray()[0];

        Instantiate(selectAttack.effect, activeBattler[targetidx].transform.position, activeBattler[targetidx].transform.rotation);
        Instantiate(enemyAttackEffect, activeBattler[currentTurn].transform.position, activeBattler[currentTurn].transform.rotation);

        DealDamage(targetidx, selectAttack.movePower);
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);

        NextTurn();
    }
    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        var enemies = activeBattler.Select((item, index) => new { idx = index, Item = item }).Where(battler => !battler.Item.isPlayer && !battler.Item.isDead).ToArray();

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (enemies.Length > i)
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = enemies[i].idx;
                targetButtons[i].targetName.text = enemies[i].Item.charName;
            }
            else
                targetButtons[i].gameObject.SetActive(false);
        }
    }
    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);
        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeBattler[currentTurn].movesAvailabe.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattler[currentTurn].movesAvailabe[i];
                magicButtons[i].nameText.text = activeBattler[currentTurn].movesAvailabe[i];

                var selectSpell = moves.Where(move => move.moveName == activeBattler[currentTurn].movesAvailabe[i]).ToArray()[0];
                magicButtons[i].spellCost = selectSpell.moveCost;
                magicButtons[i].costText.text = selectSpell.moveCost.ToString();
            }
            else
                magicButtons[i].gameObject.SetActive(false);
        }
    }
    public void Flee()
    {
        int fleeRnd = Random.Range(0, 100);

        if (fleeRnd < chanceToFlee)
        {
            flee = true;
            StartCoroutine(EndBattleCo());
        }
        else
        {
            battleNotification.awakeTime = 1f;
            battleNotification.Activate("Couldn't Escape");
            NextTurn();
        }
    }
    public void OpenItemMenu()
    {
        ShowItem();
        itemButtons[0].Press();
        itemsMenu.SetActive(true);
    }
    public void ShowItem()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "" && GameManager.instance.GetItem(GameManager.instance.itemsHeld[i]).isItem)
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItem(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectItem(Item item)
    {
        selectedItem = item;
        itemName.text = selectedItem.itemName;
        itemDescription.text = selectedItem.description;
    }
    public void UseItem()
    {
        selectedItem.UseBattle(currentTurn);

        itemsMenu.SetActive(false);
        NextTurn();
    }
    public IEnumerator EndBattleCo()
    {
        battleActive = false;

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);
        statsMenu.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);

        foreach (var battler in activeBattler)
        {
            if (battler.isPlayer)
            {
                var playerstats = GameManager.instance.playerStats.Where(player => player.charName == battler.charName).ToArray()[0];
                playerstats.currentHP = battler.currentHP;
                playerstats.currentMP = battler.currentMP;
            }

            Destroy(battler.gameObject);
        }

        activeBattler.Clear();
        currentTurn = 0;
        battleScene.SetActive(false);
        UIFade.instance.FadeFromBlack();
        if (flee)
            GameManager.instance.battleActive = false;
        else
            BattleReward.instance.OpenRewardScreen(rewardXP, rewardItems);

    }
    public IEnumerator GameOverCo()
    {
        battleActive = false;

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);
        statsMenu.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        foreach (var battler in activeBattler)
        {
            if (battler.isPlayer)
            {
                var playerstats = GameManager.instance.playerStats.Where(player => player.charName == battler.charName).ToArray()[0];
                playerstats.currentHP = battler.currentHP;
                playerstats.currentMP = battler.currentMP;
            }

            Destroy(battler.gameObject);
        }

        activeBattler.Clear();
        currentTurn = 0;
        battleScene.SetActive(false);

        SceneManager.LoadScene(gameOverScene);
    }
}
