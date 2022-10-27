using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public List<string> questToCheck;
    public List<bool> activeIfTrue;
    private bool initialCheckDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            CheckQuests();
            initialCheckDone = true;
        }
    }
    public void CheckQuests()
    {
        for (int i = 0; i < questToCheck.Count; i++)
            if (QuestManager.instance.CheckIfComplete(questToCheck[i]))
                objectToActivate.SetActive(activeIfTrue[i]);
    }
}
