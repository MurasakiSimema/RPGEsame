using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToArea : MonoBehaviour
{
    public string Area;
    public string AreaTransitionName;

    public FromArea Entrance;

    // Start is called before the first frame update
    void Start()
    {
        Entrance.TransitionName = AreaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(Area);

            PlayerController.instance.AreaTransitionName = AreaTransitionName;
        }
    }
}
