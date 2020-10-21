using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    public KeyCode flapButton;
    public float upForce;
    public bool isDead = false;

    private bool gameStarted;

    private Rigidbody2D rb2D;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!isDead)
        {
            if (Input.GetKeyDown(flapButton))
            {
                rb2D.isKinematic = false;

                anim.SetTrigger("Flap");
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(new Vector2(0, upForce));

                //If it is the first flap
                if (!gameStarted)
                {
                    gameStarted = true;
                }
            }
        }

        //If the game is over and we still flapped;
        if (isDead && Input.GetKeyDown(flapButton))
        {
            //GameController.instance.RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        anim.SetTrigger("Die");

        rb2D.velocity = Vector2.zero;
    }
}
