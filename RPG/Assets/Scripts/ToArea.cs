using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToArea : MonoBehaviour
{
    public string Area;
    public string areaTransitionName;

    //public FromArea Entrance;

    // Start is called before the first frame update
    void Start()
    {
        //Entrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter2D is called when a collision happens
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")                                              // Se la collisione è con il player
        {
            SceneManager.LoadScene(Area);                                           
            PlayerController.instance.areaTransitionName = areaTransitionName;      
        }
    }
}
