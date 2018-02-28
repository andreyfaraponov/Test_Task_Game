#define DEBUG
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class		Game_Script : MonoBehaviour {
	public string wordToCollect;
	public GameObject placeholderPref;
	public GameObject letterPref;
	public GameObject inputPanel;
	public GameObject playerPref;
	public Transform canvas;
	private float halfCanvasWidth;
	private float halfCanvasHeigth;
	private Stack<GameObject> itemsForCompare;
	private GameObject current;
	public GameObject[] itemsArray;
	private GameObject[] inputPlacesArray;
	private float width;
	/// <summary>
	/// Instantiate all available items with letters from word to collect
	/// </summary>
	void Start()
	{
		RectTransform panel;
		ItemBehaviour tmp;

		wordToCollect = wordToCollect.ToUpper();
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
			tmp.GetComponentInChildren<Text>().text = wordToCollect[i].ToString();
		}
		panel = inputPanel.GetComponent<RectTransform>();
		width = wordToCollect.Length * (letterPref
			.GetComponentInChildren<ItemBehaviour>()
			.GetComponent<RectTransform>()
			.rect.width + 5) + 5;
		panel.sizeDelta = new Vector2(width, letterPref.GetComponentInChildren<ItemBehaviour>().GetComponent<RectTransform>().rect.height + 10);
		Array.Reverse(inputPlacesArray);
		itemsForCompare = new Stack<GameObject>(inputPlacesArray);
		current = itemsForCompare.Count == 1 ? itemsForCompare.Peek() : itemsForCompare.Pop();
		PlayerInstantiate();
	}
	/// <summary>
	/// Instatiate Player Object on bottom right place
	/// in window
	/// </summary>
	private void PlayerInstantiate()
	{
		GameObject tmp;

		tmp = Instantiate(playerPref, new Vector3(), Quaternion.identity);
		tmp.transform.SetParent(canvas);
		tmp.transform.position = new Vector3(halfCanvasWidth * 2 * 0.9f, halfCanvasHeigth * 2 * 0.1f, 0f);
	}
	/// <summary>
	/// Check next available place for item(letter) by letter
	/// and return position where item will be placed
	/// </summary>
	/// <param name="letter">Letter in string for compare</param>
	/// <returns></returns>
	public Transform CheckAndMoveCurrent(string letter)
	{
		if (letter == current.GetComponentInChildren<Text>().text) {
			Transform resultPosition;

			resultPosition = current.transform;
			current = itemsForCompare.Count == 1 ? itemsForCompare.Peek() : itemsForCompare.Pop();
			return (resultPosition);
		}
		return (null);
	}
	/// <summary>
	/// Reset current scene by loading new scene
	/// </summary>
	public void			ResetLevel()
	{
		SceneManager.LoadScene(0);
	}
}
