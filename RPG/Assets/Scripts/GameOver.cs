using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.gameObject.SetActive(false);
        GameMenu.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
