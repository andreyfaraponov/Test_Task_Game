using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct Limits
{
	public float xMax;
	public float xMin;
	public float yMax;
	public float yMin;
}
public class PlayerMove : MonoBehaviour {
	public bool isMove { get; private set; }
	private RectTransform canvas;
	private Limits playerMoveLimits;
	/// <summary>
	/// Canvas detection and setting limits for player move
	/// </summary>
	void Start () {
		isMove = false;
		canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
		playerMoveLimits.xMax = canvas.rect.width - 15;
		playerMoveLimits.yMax = canvas.rect.height - 15;
		playerMoveLimits.xMin = 15;
		playerMoveLimits.yMin = 15;
	}
	/// <summary>
	/// Check user click on player shape
	/// </summary>
	void FixedUpdate () {
		if (isMove)
		{
			transform.position = Input.mousePosition;
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, playerMoveLimits.xMin, playerMoveLimits.xMax),
				Mathf.Clamp(transform.position.y, playerMoveLimits.yMin, playerMoveLimits.yMax),
				0
			);
		}
	}
	/// <summary>
	/// Detect player MouseButtonDown for move
	/// </summary>
	public void Click()
	{
		isMove = true;
	}
	/// <summary>
	/// Detect player MouseButtonUp for stop moving
	/// </summary>
	public void Exit()
	{
		isMove = false;
	}
}
