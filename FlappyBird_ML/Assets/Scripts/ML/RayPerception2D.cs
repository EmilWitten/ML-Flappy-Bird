using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ray perception component. Attach this to agents to enable "local perception"
/// via the use of ray casts directed outward from the agent. 
/// </summary>
public class RayPerception2D : MonoBehaviour
{
    public LayerMask hitmask;
    List<float> perceptionBuffer = new List<float>();
    Vector3 endPosition;

    int i = 0;
    /// <summary>
    /// Creates perception vector to be used as part of an observation of an agent.
    /// </summary>
    /// <returns>The partial vector observation corresponding to the set of rays</returns>
    /// <param name="rayDistance">Radius of rays</param>
    /// <param name="rayAngles">Anlges of rays (starting from (1,0) on unit circle).</param>
    /// <param name="detectableObjects">List of tags which correspond to object types agent can see</param>
    /// <param name="startOffset">Starting heigh offset of ray from center of agent.</param>
    /// <param name="endOffset">Ending height offset of ray from center of agent.</param>
    public List<float> Perceive(float rayDistance, float[] rayAngles, string[] detectableObjects)
    {
        perceptionBuffer.Clear();

        // For each ray sublist stores categorial information on detected objects
        // along with object distance.
        foreach (float angle in rayAngles)
        {
            endPosition = transform.TransformDirection(PolarToCartesian(rayDistance, angle));

			if (Application.isEditor)
			{
				Debug.DrawRay(transform.position, endPosition, Color.black, 0.01f, true);
			}

			float[] hitObejct = new float[2];

            RaycastHit2D hit = Physics2D.Raycast(transform.position, endPosition, rayDistance, hitmask);

            if (hit.collider != null)
            {
                for (int i = 0; i < detectableObjects.Length; i++)
                {
                    if (hit.collider.gameObject.CompareTag(detectableObjects[i]))
                    {
                        hitObejct[0] = i;
                        hitObejct[1] = hit.distance;
                        break;
                    }
                }
            }
            else
            {
                hitObejct[0] = -1f;
                hitObejct[1] = -1f;
            }
            perceptionBuffer.AddRange(hitObejct);
        }
        return perceptionBuffer;
    }

	public List<Vector3> PerceivePosition(float rayDistance, float[] rayAngles)
	{
		List<Vector3> positions = new List<Vector3>();

		// For each ray sublist stores categorial information on detected objects
		// along with object distance.
		foreach (float angle in rayAngles)
		{
			endPosition = PolarToCartesian(rayDistance, angle);
			endPosition += transform.position;

			positions.Add(endPosition);
		}
		return positions;
	}

	/// <summary>
	/// Converts polar coordinate to cartesian coordinate.
	/// </summary>
	public static Vector2 PolarToCartesian(float radius, float angle)
    {
        float x = radius * Mathf.Cos(DegreeToRadian(angle));
        float y = radius * Mathf.Sin(DegreeToRadian(angle));
        return new Vector2(x, y);
    }

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    public static float DegreeToRadian(float degree)
    {
        return degree * Mathf.PI / 180f;
    }
}
