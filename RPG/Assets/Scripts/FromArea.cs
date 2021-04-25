using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromArea : MonoBehaviour
{
    public string TransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (TransitionName == PlayerController.instance.AreaTransitionName)
            PlayerController.instance.transform.position = transform.position;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
