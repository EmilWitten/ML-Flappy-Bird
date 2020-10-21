using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAgent : Agent {

    public float flapForce;


    private Animator anim;
    private Rigidbody2D rb2D;
    private RayPerception2D perception2D;
	private BirdSounds sounds;

    private Vector3 startPos;

    private float[] angles;
    private string[] detectableObjects;

	public override void InitializeAgent()
    {
        perception2D = GetComponent<RayPerception2D>();
		sounds = GetComponent<BirdSounds>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startPos = transform.position;

        angles = new float[] {60, 45, 30, 0, 330, 315, 300};
        detectableObjects = new string[] {"ground", "sky", "wall", "goal"};
    }

    public override void CollectObservations()
    {
        // Agent info
        AddVectorObs(transform.position.y);
        AddVectorObs(rb2D.velocity.y);

		// Environment info
		AddVectorObs(perception2D.Perceive(4f, angles, detectableObjects));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        switch ((int)vectorAction[0])
        {
            case 0:
                //Idle
                break;
            case 1:
                //Flap
                Flap();
                break;
            default:
                break;
        }

        AddReward(0.01f);

    }

    public override void AgentReset()
    {
		sounds.PlayHurtSound();

        transform.position = startPos;
        rb2D.velocity = Vector2.zero;

        GameController.instance.GameReset();
    }

    public void Flap()
    {
		sounds.PlayWingFlap();
        anim.SetTrigger("Flap");
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(new Vector2(0, flapForce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddReward(1f);

        GameController.instance.AddToScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.velocity = Vector2.zero;

        AddReward(-1f);
        Done();
    }
}
