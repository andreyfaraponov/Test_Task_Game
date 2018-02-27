#define DEBUG
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Rand = UnityEngine.Random;

[System.Serializable]
public struct		SpeedLimits
{
	public float				highSpeed;
	public float				lowSpeed;
}
public class		Game_Script : MonoBehaviour {
	public string				word;
	public GameObject			placeholder_pref;
	public GameObject			item_pref;
	public GameObject			inputPanel;
	public GameObject			player_pref;
	public Transform			canvas;
	public GameObject			path_pref;
	public GameObject			point_pref;
	public GameObject			startButton;
	public GameObject			resetButton;
	public InputField			input;
	float						halfCanvasWidth;
	float						halfCanvasHeigth;
	public Stack<GameObject>	chars;
	public SpeedLimits			limits;
	private GameObject			current;
	GameObject[]				tmp_arr;
	GameObject					tmp_item;
	float						width;

	public void			StartGame()
	{
		RectTransform			panel;

		startButton.SetActive(false);
		input.gameObject.SetActive(false);
		resetButton.SetActive(true);
		if (input.text.Length > 0)
		{
			word = input.text;
			word = word.ToUpper();
		}
		tmp_arr = new GameObject[word.Length];
		halfCanvasHeigth = canvas.GetComponent<RectTransform>().rect.height / 2;
		halfCanvasWidth = canvas.GetComponent<RectTransform>().rect.width / 2;
		for(int i = 0; i < word.Length; i++)
			tmp_arr[i] = LetterInit(i);
		panel = inputPanel.GetComponent<RectTransform>();
		width = word.Length * 55 + 5;
		panel.sizeDelta = new Vector2(width, 0);
		Array.Reverse(tmp_arr);
		chars = new Stack<GameObject>(tmp_arr);
		current = chars.Pop();
		PlayerInit();
	}
	private GameObject	LetterInit(int i)
	{
		GameObject	tmp;

		tmp = Instantiate(placeholder_pref, new Vector3(), Quaternion.identity);
		tmp.GetComponentInChildren<Text>().text = word[i].ToString();
		tmp_item = Instantiate(item_pref, new Vector3(), Quaternion.identity);
		tmp_item.GetComponentInChildren<Text>().text = word[i].ToString();
		tmp_item.transform.SetParent(canvas);
		GenerateAndSetPath(tmp_item.GetComponent<ItemBehaviour>());
		tmp_item.transform.position = new Vector3(60 + i * 60, 60 + i * 60, 0);
		tmp_item.GetComponent<ItemBehaviour>().goTo = tmp.transform;
		tmp_item.GetComponent<ItemBehaviour>().SetSpeed(Rand.Range(limits.lowSpeed, limits.highSpeed));
		tmp.transform.SetParent(inputPanel.transform);
		return (tmp);
	}
	private void		PlayerInit()
	{
		GameObject		tmp;

		tmp = Instantiate(player_pref, new Vector3(), Quaternion.identity);
		tmp.transform.SetParent(canvas);
		tmp.transform.position = new Vector3(halfCanvasWidth * 2 * 0.9f, halfCanvasHeigth * 2 * 0.1f, 0f);
	}
	public bool			CheckAndMoveCurrent(string str)
	{
		if (str == current.GetComponentInChildren<Text>().text)
			return (current = chars.Count == 1 ? chars.Peek() : chars.Pop());
		return (false);
	}
	private void		GenerateAndSetPath(ItemBehaviour item)
	{
		PathScript		scr;
		GameObject		path;

		path = Instantiate(path_pref, new Vector3(), Quaternion.identity);
		path.transform.SetParent(canvas);
		path.transform.position = new Vector3(halfCanvasWidth, halfCanvasHeigth, 0);
		scr = path.GetComponent<PathScript>();
		for (int i = 0; i < 4; i++)
			scr.points[i] = Point_Generator(i + 1, path.transform);
		item.path = path;
		item.TryToGo();
	}
	private GameObject	Point_Generator(int direction, Transform path)
	{
		GameObject		point;

		point = Instantiate(point_pref, new Vector3(), Quaternion.identity);
		point.transform.SetParent(path.transform);
		switch(direction) {
			case 1: point.transform.position = new Vector3(
				-halfCanvasWidth - Range() + halfCanvasWidth,
				Rand.Range(-halfCanvasHeigth, halfCanvasHeigth) + halfCanvasHeigth,
				0
			);
				break;
			case 2: point.transform.position = new Vector3(
				Rand.Range(-halfCanvasWidth, halfCanvasWidth) + halfCanvasWidth,
				-halfCanvasHeigth - Range() + halfCanvasHeigth,
				0
			);
				break;
			case 3: point.transform.position = new Vector3(
				halfCanvasWidth + Range() + halfCanvasWidth,
				Rand.Range(-halfCanvasHeigth, halfCanvasHeigth) + halfCanvasHeigth,
				0
			);
				break;
			case 4: point.transform.position = new Vector3(
				Rand.Range(-halfCanvasWidth, halfCanvasWidth) + halfCanvasWidth,
				halfCanvasHeigth +  Range() + halfCanvasHeigth,
				0
			);
				break;
			default: point.transform.position = new Vector3(0, 0, 0);
				break;
		}
		return (point);
	}
	public void			Reset()
	{
		SceneManager.LoadScene(0);
	}
	private int			Range()
	{
		return (Rand.Range(50, 100));
	}
}
