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

	private float zoomPerSecond = 2.0f;
	private float fadePerSecond = 2.5f;
	private bool fadingIn, fadingOut;
	private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Color transparent = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	Vector3 offset = new Vector3(0, 80, 0);

	// TODO: These'll be in a JSON later
	string[] lines = new string[] { "First text", "Really really really really long text", "Third text", "Fourth text", };
	string[] speakers = new string[] {"Etrelle", "Bonefish", "Etrelle", "Bonefish"};

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

	public void StartScene (int index) {
		bubbleIndex = index;
		sceneRunning = true;

		StartCoroutine("NextWindow");

		ZoomIn();
	}

	public void EndScene() {
		sceneRunning = false;

		// TODO Cleanup old windows, all should have name SpeechBubble(Clone)

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
			if (cam.orthographicSize <= sceneZoom) {
				zoomingIn = false;
			}
		}

		if (zoomingOut) {
			cam.orthographicSize += (zoomPerSecond * Time.deltaTime);
			if (cam.orthographicSize >= defaultZoom) {
				zoomingOut = false;
			}
		}
		// Check for input, advance text if left click

		if (sceneRunning) {
			if ((!fadingIn) && (!fadingOut) && (!zoomingIn) && (!zoomingOut) && (Input.GetMouseButtonDown (0))) {
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

	void DisplayText(int index) {
		GameObject speaker = GameObject.Find (speakers [bubbleIndex]);
		activeBubble = Instantiate(bubble, speaker.transform.position + offset, Quaternion.identity);

		// Convert from world position to canvas position.
		// TODO: This isn't working quite as well as it needs to...
		// It's too far away as you approach the screen's edges.
		RectTransform CanvasRect = activeBubble.GetComponent<RectTransform>();
		Vector3 pos = speaker.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint (pos);
		CanvasRect.anchorMin = viewportPoint;
		CanvasRect.anchorMax = viewportPoint;

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
		// Display the next window, if there is one to display
		yield return new WaitForSeconds (0.4f);

		if (bubbleIndex > 0) {
			activeBubble.gameObject.SetActive (false);
		}

		fadingOut = false;

		if (bubbleIndex < lines.Length) {
			//Vector3 nextLocation = _SpeakerPosition ();
			DisplayText (bubbleIndex);
		} else {
			EndScene ();
		}
			
		bubbleIndex++;
	}

	private Vector3 _SpeakerPosition() {
		string nextSpeakerName = speakers [bubbleIndex];
		//print (nextSpeakerName);
		// TODO Get them by tag instead?
		Vector3 nextLocation = GameObject.Find (nextSpeakerName).transform.position;
		//print (nextLocation);
		return nextLocation;
	}
		
}
