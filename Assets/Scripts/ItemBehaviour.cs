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
	private int index;
	public int placeHolderHash { private get; set; }

	void Start()
	{
		gameController = GameObject.FindWithTag("GameController").GetComponent<Game_Script>();
		GoOnByCheckpoints();
	}
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
				index = (index + 1) < points.Length ? (index + 1) : 0;
				currentCheckPoint = points[index];
			}
		}
	}
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && other.GetComponent<PlayerMove>().IsMove() &&
			gameController.CheckAndMoveCurrent(placeHolderHash))
		{
			gameObject.GetComponent<Collider2D>().enabled = false;
			currentCheckPoint = null;
			goToPlace = true;
		}
	}
	public void GoOnByCheckpoints()
	{
		SRandom rnd;

		rnd = new SRandom();
		points = points.OrderBy(x => rnd.Next()).ToArray();
		currentCheckPoint = points[index];
	}
}
