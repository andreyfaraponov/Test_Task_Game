using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

using SRandom = System.Random;

public class ItemBehaviour : MonoBehaviour {
	public Transform placePosition;
	public float speed = 20f;
	private bool goToPlace;
	private GameObject currentCheckPoint;
	private Game_Script gameController;
	public GameObject[] points;
	private int currentPointIndex;
	public int placeHolderHash { private get; set; }
	/// <summary>
	/// Find and Get GameController Game_Script
	/// </summary>
	void Start()
	{
		gameController = GameObject.FindWithTag("GameController").GetComponent<Game_Script>();
		GoOnByCheckpoints();
	}
	/// <summary>
	/// Product of motion between path points or placeholder in inputPanel
	/// </summary>
	void FixedUpdate()
	{
		if (goToPlace)
		{
			transform.position = Vector3.Lerp(transform.position, placePosition.position, 0.1f);
			if (Vector3.Distance(transform.position, placePosition.position) < 0.1f)
				goToPlace = false;
		}
		else if (currentCheckPoint)
		{
			transform.position += (currentCheckPoint.transform.position - transform.position).normalized *
				speed * Time.deltaTime * 10f;
			if (Vector3.Distance(transform.position, currentCheckPoint.transform.position) < 20f)
			{
				currentPointIndex = (currentPointIndex + 1) < points.Length ? (currentPointIndex + 1) : 0;
				currentCheckPoint = points[currentPointIndex];
			}
		}
	}
	/// <summary>
	/// Change of speed between points
	/// </summary>
	/// <param name="newSpeed">new speed for motion between path points</param>
	public void SetSpeed(float newSpeed)
	{
		this.speed = newSpeed;
	}
	/// <summary>
	/// Checking the player collision with letter and
	/// current need letter in inputPanel
	/// </summary>
	/// <param name="collisionObj"></param>
	void OnTriggerEnter2D(Collider2D collisionObj)
	{
		if (collisionObj.CompareTag("Player") && collisionObj.GetComponent<PlayerMove>().isMove &&
			gameController.CheckAndMoveCurrent(placeHolderHash))
		{
			gameObject.GetComponent<Collider2D>().enabled = false;
			currentCheckPoint = null;
			goToPlace = true;
		}
	}
	/// <summary>
	/// Start Letter move by its checkpoints
	/// with the first random point
	/// </summary>
	public void GoOnByCheckpoints()
	{
		SRandom rnd;

		rnd = new SRandom();
		points = points.OrderBy(x => rnd.Next()).ToArray();
		currentCheckPoint = points[currentPointIndex];
	}
}
