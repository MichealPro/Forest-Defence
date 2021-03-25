using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

	//public static Transform[] points;

	//const float waypointGizmoRadius = 0.3f;

	//void Awake()
	//{
	//	points = new Transform[transform.childCount];
	//	for (int i = 0; i < points.Length; i++)
	//	{
	//		points[i] = transform.GetChild(i);
	//	}
	//}

	public int GetNextIndex(int i)
	{
		return i + 1;
	}

	public Vector3 GetWaypoint(int i)
	{
		return transform.GetChild(i).position;
	}
}

