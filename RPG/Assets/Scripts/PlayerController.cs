using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public float MoveSpeed;

    public Animator Animator;

    public static PlayerController instance;


    public string AreaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * MoveSpeed;

        Animator.SetFloat("MoveX", RigidBody.velocity.x);
        Animator.SetFloat("MoveY", RigidBody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            Animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            Animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
