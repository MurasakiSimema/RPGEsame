using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        if (PlayerController.instance == null)          //Se non esiste già un player
            Instantiate(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
