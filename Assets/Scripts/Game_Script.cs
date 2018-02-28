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
		width = wordToCollect.Length * (letterPref.GetComponentInChildren<ItemBehaviour>().GetComponent<RectTransform>().rect.width + 5) + 5;
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
	/// Check next available place for letter by placeholder object Hash
	/// </summary>
	/// <param name="placeholderHash">Placeholder object Hash for compare</param>
	/// <returns></returns>
	public bool CheckAndMoveCurrent(int placeholderHash)
	{
		if (placeholderHash == current.GetHashCode())
			return (current = itemsForCompare.Count == 1 ? itemsForCompare.Peek() : itemsForCompare.Pop());
		return (false);
	}
	/// <summary>
	/// Reset current by loading first scene
	/// </summary>
	public void			Reset()
	{
		SceneManager.LoadScene(0);
	}
}
