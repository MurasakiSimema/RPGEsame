using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<string> questMarkersNames;
    public List<bool> questMarkerStatus;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkerStatus = new List<bool>(new bool[questMarkersNames.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfComplete(string questName)
    {
        int idx = questMarkersNames.IndexOf(questName);
        if (idx >= 0)
            return questMarkerStatus[idx];
        Debug.LogError("Quest: " + questName + " does not exist");
        return false;
    }
    public void MarkQuestTrue(string questName)
    {
        int idx = questMarkersNames.IndexOf(questName);
        if (idx >= 0)
            questMarkerStatus[idx] = true;

        UpdateLocalQuestObjects();
    }
    public void MarkQuestFalse(string questName)
    {
        int idx = questMarkersNames.IndexOf(questName);
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
    public void SaveQuestData()
    {
        for(int i = 0; i < questMarkersNames.Count; i++)
            PlayerPrefs.SetInt("QuestMarker_" + questMarkersNames[i], Convert.ToInt32(questMarkerStatus[i]));
    }
    public void LoadQuestData()
    {
        for(int i = 0; i < questMarkersNames.Count; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkersNames[i]))
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkersNames[i]);
            questMarkerStatus[i] = valueToSet > 0;
        }
    }
}
