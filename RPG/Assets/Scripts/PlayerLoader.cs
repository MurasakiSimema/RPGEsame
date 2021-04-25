using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    void Start()
    {
        if (PlayerController.instance == null)
            Instantiate(Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
