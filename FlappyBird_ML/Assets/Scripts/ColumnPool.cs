using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {

    public GameObject columnPrefab;
    public int ColumnPoolSize = 5;
    public float spawnRate = 4f;
    public float columnMin = -0.1f;
    public float columnMax = 0.45f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-25, -15);
    private float timeSinceLastSpawn;
    private float spawnPosX = 2f;
    private int currentColumn = 0;


	// Use this for initialization
	void Start ()
    {
        columns = new GameObject[ColumnPoolSize];

        for(int i = 0; i < ColumnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity, gameObject.transform);
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawn += Time.unscaledDeltaTime;
        
        if(!GameController.instance.IsGameOver && timeSinceLastSpawn > spawnRate)
        {
            timeSinceLastSpawn = 0;
            float spawnPosY = Random.Range(columnMin, columnMax);


            columns[currentColumn].transform.position = new Vector2(spawnPosX, spawnPosY);
            currentColumn++;

            if(currentColumn >= columns.Length)
            {
                currentColumn = 0;
            }
        }
	}

    public void ColumnReset()
    {
        for(int i = 0; i < columns.Length; i++)
        {
            columns[i].transform.position = objectPoolPosition;
        }

        timeSinceLastSpawn = 0;
    }
}
