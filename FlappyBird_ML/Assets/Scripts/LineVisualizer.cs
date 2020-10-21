using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualizer : MonoBehaviour {

	private RayPerception2D perception2D;
	private float[] angles;
	private List<Vector3> obsPositions;
	private LineRenderer lines;

	// Use this for initialization
	void Start () { 
		perception2D = GetComponent<RayPerception2D>();
		angles = new float[] { 60, 45, 30, 0, 330, 315, 300 };

		lines = GetComponent<LineRenderer>();
		lines.positionCount = angles.Length * 2;
	}

	private void Update()
	{
		obsPositions = perception2D.PerceivePosition(4f, angles);
		int j = 0;

		for(int i = 0; i < lines.positionCount; i++)
		{
			if(i % 2 == 0)
			{
				lines.SetPosition(i, transform.position);
			}
			else
			{
				lines.SetPosition(i, obsPositions[j]);
				j++;
			}
		}

	}
}
