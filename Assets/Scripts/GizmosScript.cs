using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosScript : MonoBehaviour {

	GameObject[] points;
	void Start()
	{
		points = gameObject.GetComponentInChildren<ItemBehaviour>().points;
	}
	void OnDrawGizmos()//Selected()
	{
		points = gameObject.GetComponentInChildren<ItemBehaviour>().points;
		for (int i = 0; i < points.Length; i++)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawCube(points[i].transform.position, Vector3.one * 30);
			if (i < (points.Length - 1))
				Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
			else
				Gizmos.DrawLine(points[i].transform.position, points[0].transform.position);
		}
	}
}
