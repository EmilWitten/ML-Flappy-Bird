using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    private Rigidbody2D rb2D;


	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {


  //      if (GameController.instance.IsGameStarted == true)
  //      {

  //      }

		//if(GameController.instance.IsGameOver == true)
  //      {
  //          rb2D.velocity = Vector2.zero;
  //      }
	}
}
