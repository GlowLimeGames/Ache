using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
	public Canvas canv;
	public Transform bubble;   // the prefab
	public Transform activeBubble;
	public int bubbleIndex;

	[SerializeField] private float fadePerSecond = 2.5f;
	private bool fadingIn, fadingOut;
	private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Color transparent = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	string[] lines = new string[] { "First text", "Second text", "Third text", "Fourth text", };

	// Use this for initialization
	void Start () {
		fadingIn = fadingOut = false;
		bubbleIndex = 0;
		DisplayText (bubbleIndex);
	}
	
	// Update is called once per frame
	void Update () {
		// Check for input, advance text if left click
		if (Input.GetMouseButtonDown (0)) {
			RemoveText ();
			bubbleIndex++;
		}

		if (fadingIn) {
			Material material = activeBubble.gameObject.GetComponentInChildren<Image> ().material;
			Color color = material.color;
			material.color = new Color(color.r, color.g, color.b, color.a + (fadePerSecond * Time.deltaTime));
		}

		if (fadingOut) {
			Material material = activeBubble.gameObject.GetComponentInChildren<Image> ().material;
			Color color = material.color;
			material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
		}
	}

	void DisplayText(int index) {
		activeBubble = Instantiate(bubble, new Vector3(70, -60, 0), Quaternion.identity);
		activeBubble.gameObject.GetComponentInChildren<Image> ().material.color = white;

		Text bubbleText = activeBubble.GetComponentInChildren<Text>();
		bubbleText.text = lines [index];
		activeBubble.SetParent (canv.transform, false);
		activeBubble.gameObject.SetActive (true);

		activeBubble.gameObject.GetComponentInChildren<Image> ().material.color = transparent;
		fadingIn = true;
		StartCoroutine ("StopFadeIn");
	}

	void RemoveText() {
		fadingOut = true;

		StartCoroutine("NextWindow");
	}

	IEnumerator StopFadeIn() {
		yield return new WaitForSeconds (0.7f);
		fadingIn = false;
	}
		
	IEnumerator NextWindow() {
		yield return new WaitForSeconds (0.7f);
		activeBubble.gameObject.SetActive (false);
		fadingOut = false;
		DisplayText (bubbleIndex);

	}
}
