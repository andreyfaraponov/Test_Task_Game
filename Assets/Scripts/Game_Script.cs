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
	public string wordToCollect;
	public GameObject placeholderPref;
	public GameObject letterPref;
	public GameObject inputPanel;
	public GameObject playerPref;
	public Transform canvas;
	public GameObject resetButton;
	private float halfCanvasWidth;
	private float halfCanvasHeigth;
	private Stack<GameObject> itemsForCompare;
	public SpeedLimits limits;
	private GameObject current;
	public GameObject[] itemsArray;
	private GameObject[] inputPlacesArray;
	private GameObject tmpItem;
	private float width;

	void Start()
	{
		RectTransform panel;
		ItemBehaviour tmp;

		itemsForCompare = new Stack<GameObject>();
		inputPlacesArray = new GameObject[wordToCollect.Length];
		halfCanvasHeigth = canvas.GetComponent<RectTransform>().rect.height / 2;
		halfCanvasWidth = canvas.GetComponent<RectTransform>().rect.width / 2;
		for (int i = 0; i < wordToCollect.Length; i++)
		{
			inputPlacesArray[i] = Instantiate(placeholderPref, new Vector3(), Quaternion.identity);
			inputPlacesArray[i].transform.SetParent(inputPanel.transform);
			inputPlacesArray[i].GetComponentInChildren<Text>().text = wordToCollect[i].ToString();
			tmp = itemsArray[i].GetComponentInChildren<ItemBehaviour>();
			tmp.placePosition = inputPlacesArray[i].transform;
			tmp.GetComponentInChildren<Text>().text = wordToCollect[i].ToString();
			tmp.placeHolderHash = inputPlacesArray[i].GetHashCode();
		}
		panel = inputPanel.GetComponent<RectTransform>();
		width = wordToCollect.Length * (letterPref.GetComponent<RectTransform>().rect.width + 5) + 5;
		panel.sizeDelta = new Vector2(width, letterPref.GetComponent<RectTransform>().rect.height + 10);
		Array.Reverse(inputPlacesArray);
		itemsForCompare = new Stack<GameObject>(inputPlacesArray);
		current = itemsForCompare.Count == 1 ? itemsForCompare.Peek() : itemsForCompare.Pop();
		PlayerInstantiate();
	}
	private void PlayerInstantiate()
	{
		GameObject tmp;

		tmp = Instantiate(playerPref, new Vector3(), Quaternion.identity);
		tmp.transform.SetParent(canvas);
		tmp.transform.position = new Vector3(halfCanvasWidth * 2 * 0.9f, halfCanvasHeigth * 2 * 0.1f, 0f);
	}
	public bool CheckAndMoveCurrent(int placeholderHash)
	{
		if (placeholderHash == current.GetHashCode())
			return (current = itemsForCompare.Count == 1 ? itemsForCompare.Peek() : itemsForCompare.Pop());
		return (false);
	}
	// private GameObject LetterInit(int i)
	// {
	// 	GameObject tmp;

	// 	tmp = Instantiate(placeholderPref, new Vector3(), Quaternion.identity);
	// 	tmp.GetComponentInChildren<Text>().text = wordToCollect[i].ToString();
	// 	tmpItem = Instantiate(letterPref, new Vector3(), Quaternion.identity);
	// 	tmpItem.GetComponentInChildren<Text>().text = wordToCollect[i].ToString();
	// 	tmpItem.transform.SetParent(canvas);
	// 	GenerateAndSetPath(tmpItem.GetComponent<ItemBehaviour>());
	// 	tmpItem.transform.position = new Vector3(60 + i * 60, 60 + i * 60, 0);
	// 	tmpItem.GetComponent<ItemBehaviour>().goTo = tmp.transform;
	// 	tmpItem.GetComponent<ItemBehaviour>().SetSpeed(Rand.Range(limits.lowSpeed, limits.highSpeed));
	// 	tmp.transform.SetParent(inputPanel.transform);
	// 	return (tmp);
	// }
	// private void GenerateAndSetPath(ItemBehaviour item)
	// {
	// 	PathScript scr;
	// 	GameObject path;

	// 	path = Instantiate(pathPref, new Vector3(), Quaternion.identity);
	// 	path.transform.SetParent(canvas);
	// 	path.transform.position = new Vector3(halfCanvasWidth, halfCanvasHeigth, 0);
	// 	scr = path.GetComponent<PathScript>();
	// 	for (int i = 0; i < 4; i++)
	// 		scr.points[i] = PointGenerator(i + 1, path.transform);
	// 	item.path = path;
	// 	item.TryToGo();
	// }
	// private GameObject	PointGenerator(int direction, Transform path)
	// {
	// 	GameObject		point;

	// 	point = Instantiate(pointPref, new Vector3(), Quaternion.identity);
	// 	point.transform.SetParent(path.transform);
	// 	switch(direction) {
	// 		case 1: point.transform.position = new Vector3(
	// 			-halfCanvasWidth - Rand.Range(50, 100) + halfCanvasWidth,
	// 			Rand.Range(-halfCanvasHeigth, halfCanvasHeigth) + halfCanvasHeigth,
	// 			0
	// 		);
	// 			break;
	// 		case 2: point.transform.position = new Vector3(
	// 			Rand.Range(-halfCanvasWidth, halfCanvasWidth) + halfCanvasWidth,
	// 			-halfCanvasHeigth - Rand.Range(50, 100) + halfCanvasHeigth,
	// 			0
	// 		);
	// 			break;
	// 		case 3: point.transform.position = new Vector3(
	// 			halfCanvasWidth + Rand.Range(50, 100) + halfCanvasWidth,
	// 			Rand.Range(-halfCanvasHeigth, halfCanvasHeigth) + halfCanvasHeigth,
	// 			0
	// 		);
	// 			break;
	// 		case 4: point.transform.position = new Vector3(
	// 			Rand.Range(-halfCanvasWidth, halfCanvasWidth) + halfCanvasWidth,
	// 			halfCanvasHeigth +  Rand.Range(50, 100) + halfCanvasHeigth,
	// 			0
	// 		);
	// 			break;
	// 		default: point.transform.position = new Vector3(0, 0, 0);
	// 			break;
	// 	}
	// 	return (point);
	// }
	public void			Reset()
	{
		SceneManager.LoadScene(0);
	}
}
