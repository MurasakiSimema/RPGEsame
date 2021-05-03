using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;

    public Animator animator;

    public static PlayerController instance;


    public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)               // Se non esiste già un player
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);      // Non distruggere al cambio scena
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        animator.SetFloat("MoveX", rigidBody.velocity.x);
        animator.SetFloat("MoveY", rigidBody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
