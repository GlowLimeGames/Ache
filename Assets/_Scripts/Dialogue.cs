using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
	public Canvas canv;
	private Camera cam;
	public Transform bubble;   // the prefab

	public bool sceneRunning;
	private Transform activeBubble;
	private int bubbleIndex;

	private float zoomPerSecond = 2.5f;
	private float fadePerSecond = 2.5f;
	private bool fadingIn, fadingOut;
	private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Color transparent = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	Vector3 offset = new Vector3(-60, -38, 0);

	string[] lines = new string[] { "First text", "Second text", "Third text", "Fourth text", };

	private bool zoomingIn, zoomingOut;

	private float defaultZoom = 5.0f;
	private float sceneZoom = 4.0f;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
		sceneRunning = false;
		fadingIn = fadingOut = false;
		zoomingIn = zoomingOut = false;
		bubbleIndex = 0;
	}

	public void StartScene (int index=0) {
		bubbleIndex = index;
		sceneRunning = true;
		DisplayText (bubbleIndex, offset);

		//cam.orthographicSize = sceneZoom;
		ZoomIn();
	}

	public void EndScene() {
		sceneRunning = false;

		//cam.orthographicSize = defaultZoom;
		ZoomOut();
	}

	public void ZoomIn() {
		zoomingIn = true;
	}

	public void ZoomOut() {
		zoomingOut = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (zoomingIn) {
			cam.orthographicSize -= (zoomPerSecond * Time.deltaTime);
			if (cam.orthographicSize < sceneZoom) {
				zoomingIn = false;
			}
		}

		if (zoomingOut) {
			cam.orthographicSize += (zoomPerSecond * Time.deltaTime);
			if (cam.orthographicSize > defaultZoom) {
				zoomingOut = false;
			}
		}
		// Check for input, advance text if left click

		if (sceneRunning) {
			if ((!fadingIn) && (!fadingOut) && (Input.GetMouseButtonDown (0))) {
				RemoveText ();
			}

			if (fadingIn) {
				// Add alpha
				Material material = activeBubble.gameObject.GetComponentInChildren<Image> ().material;
				Color color = material.color;
				material.color = new Color(color.r, color.g, color.b, color.a + (fadePerSecond * Time.deltaTime));
			}

			if (fadingOut) {
				// Subtract alpha
				Material material = activeBubble.gameObject.GetComponentInChildren<Image> ().material;
				Color color = material.color;
				material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
			}
		}
			
	}

	void DisplayText(int index, Vector3 offset) {
		activeBubble = Instantiate(bubble, offset, Quaternion.identity);
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
		yield return new WaitForSeconds (0.4f);
		fadingIn = false;
	}
		
	IEnumerator NextWindow() {
		yield return new WaitForSeconds (0.4f);
		activeBubble.gameObject.SetActive (false);
		fadingOut = false;

		bubbleIndex++;
		if (bubbleIndex < lines.Length) {
			DisplayText (bubbleIndex, offset);
		} else {
			EndScene ();
		}

	}
}
