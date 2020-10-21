using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    public Transform[] tiles;

    private Vector2[] startPositions;

    private float objectWidth;
    private float screenWidth;

	// Use this for initialization
	void Start ()
    {
        startPositions = new Vector2[tiles.Length];

        for (int i = 0; i < tiles.Length; i++)
        {
            startPositions[i] = tiles[i].position;
        }

        float height = 2f * Camera.main.orthographicSize;
        screenWidth = height * Camera.main.aspect;

        objectWidth = tiles[0].transform.GetChild(0).GetComponent<BoxCollider2D>().size.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].position.x < 0 - (screenWidth/2f + objectWidth/2f))
            {
                RepositionBackground(i);
            }
        }
	}

    private void RepositionBackground(int i)
    {
        int nextTileIndex = i+1;

        // This tile is the last tile in the array
        if(nextTileIndex == tiles.Length)
        {
            nextTileIndex = 0;
        }

        Vector2 groundOffset = new Vector2(tiles[nextTileIndex].position.x + objectWidth, 0);
        tiles[i].position =  groundOffset;

    }

    public void BackgroundReset()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].position = startPositions[i];
        }
    }

}
