using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<string> questMarkeNames;
    public List<bool> questMarkerStatus;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkerStatus = new List<bool>(new bool[questMarkeNames.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfComplete(string questName)
    {
        int idx = questMarkeNames.IndexOf(questName);
        if (idx >= 0)
            return questMarkerStatus[idx];
        Debug.LogError("Quest: " + questName + " does not exist");
        return false;
    }
    public void MarkQuestTrue(string questName)
    {
        int idx = questMarkeNames.IndexOf(questName);
        if (idx >= 0)
            questMarkerStatus[idx] = true;

        UpdateLocalQuestObjects();
    }
    public void MarkQuestFalse(string questName)
    {
        int idx = questMarkeNames.IndexOf(questName);
        if (idx >= 0)
            questMarkerStatus[idx] = false;

        UpdateLocalQuestObjects();
    }
    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();
        Debug.Log(questObjects.Length);

        foreach (QuestObjectActivator questObject in questObjects)
            questObject.CheckQuests();
    }
}
