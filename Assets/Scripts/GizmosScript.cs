using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosScript : MonoBehaviour {

	public GameObject[] points;
	/// <summary>
	/// Take path points from cheldren object
	/// </summary>
	void Start()
	{
		points = gameObject.GetComponentInChildren<ItemBehaviour>().points;
	}
	/// <summary>
	/// Draw Lines between path
	/// </summary>
	void OnDrawGizmosSelected()
	{
		points = gameObject.GetComponentInChildren<ItemBehaviour>().points;
		for (int i = 0; i < points.Length; i++)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawCube(points[i].transform.position, Vector3.one * 30);
			if (i < (points.Length - 1))
				Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
			else
				Gizmos.DrawLine(points[i].transform.position, points[0].transform.position);
		}
	}
}
