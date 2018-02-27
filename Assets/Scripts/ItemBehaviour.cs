using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

using SRandom = System.Random;
using URandom = UnityEngine.Random;

public class ItemBehaviour : MonoBehaviour {
	public Transform		goTo;
	public GameObject		path;
	public float			speed;
	bool					goToPlace;
	GameObject				currentCheckPoint;
	Game_Script				gameController;
	private GameObject[]	points;
	public string					sign;
	int						index;

	void			Start()
	{
		index = (int)URandom.Range(0, 3);
		goToPlace = false;
		currentCheckPoint = null;
		gameController = GameObject.FindWithTag("GameController").GetComponent<Game_Script>();
		sign = gameObject.GetComponentInChildren<Text>().text;
		TryToGo();
	}
	void			FixedUpdate()
	{
		if (goToPlace)
		{
			transform.position = Vector3.Lerp(transform.position, goTo.position, 0.1f);
			if (Vector3.Distance(transform.position, goTo.position) < 0.1f)
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
	public void		SetSpeed(float speed)
	{
		this.speed = speed;
	}
	void			OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && other.GetComponent<PlayerMove>().IsMove() &&
			gameController.CheckAndMoveCurrent(sign))
		{
			gameObject.GetComponent<Collider2D>().enabled = false;
			currentCheckPoint = null;
			goToPlace = true;
		}
	}
	public void		TryToGo()
	{
		GameObject[]	tmp;
		SRandom			rnd;

		if (path)
		{
			tmp = path.GetComponent<PathScript>().points;
			rnd = new SRandom();
			points = tmp.OrderBy(x => rnd.Next()).ToArray();
			currentCheckPoint = points[index];
		}
	}
}
