using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromArea : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionName == PlayerController.instance.areaTransitionName)         // Se il player arriva dalla scena giusta
            PlayerController.instance.transform.position = transform.position;

        UIFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
