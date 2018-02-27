using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct Limits
{
	public float			xMax;
	public float			xMin;
	public float			yMax;
	public float			yMin;
}
public class PlayerMove : MonoBehaviour {
	bool					isMove;
	private RectTransform	canvas;
	private Limits			limits;
	void Start () {
		isMove = false;
		canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
		limits.xMax = canvas.rect.width - 15;
		limits.yMax = canvas.rect.height - 15;
		limits.xMin = 15;
		limits.yMin = 15;
	}
	void FixedUpdate () {
		if (isMove)
		{
			transform.position = Input.mousePosition;
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, limits.xMin, limits.xMax),
				Mathf.Clamp(transform.position.y, limits.yMin, limits.yMax),
				0
			);
		}
	}

	public void Click()
	{
		isMove = true;
	}
	public void Exit()
	{
		isMove = false;
	}
	public bool IsMove()
	{
		return (isMove);
	}
}
