using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(GizmosScript))]
public class PathEditor : Editor {

	GizmosScript path;
	void OnEnable()
	{
		path = (GizmosScript)target;
	}
	void OnSceneGUI()
	{
		foreach (GameObject point in path.points)
		{
			Handles.color = Color.green;
			Handles.Label(point.transform.position, "Path Point");
			Handles.DrawWireCube(point.transform.position, new Vector3(20, 20, 20));
			EditorGUI.BeginChangeCheck();
			Vector3 newPosition = Handles.PositionHandle(point.transform.position, Quaternion.identity);
			if (EditorGUI.EndChangeCheck())
			{
				point.transform.position = newPosition;
			}
		}
	}
}
