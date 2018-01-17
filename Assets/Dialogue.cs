using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Line {
	public string speaker;
	public string text;
	public string tag;
	public bool end;
}

public static class JsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.Items;
	}

	public static string ToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] Items;
	}
}

public class Dialogue : MonoBehaviour {
	public Canvas canv;
	private Camera cam;
	public Transform bubbleLeft;   // the prefabs
	public Transform bubbleRight;

	public TextAsset script;

	public bool sceneRunning;
	private Transform activeBubble;
	private int bubbleIndex;

	private float zoomPerSecond = 2.0f;
	private float fadePerSecond = 2.5f;
	private bool fadingIn, fadingOut;
	private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Color transparent = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	// Offset of speech bubble from speaker position
	//Vector3 offset = new Vector3(0, 250, 1);
	Vector3 offset;

	//static string scriptPath = "Assets/_Dialogue/Level 1 Dialogue.txt";
	string scriptJson;
	Line[] gameScript;

	private bool zoomingIn, zoomingOut;

	private float defaultZoom = 5.0f;
	private float sceneZoom = 4.0f;

	// Use this for initialization
	void Start() {
		//scriptJson = System.IO.File.ReadAllText (scriptPath);
		scriptJson = script.text;
		scriptJson = "{\"Items\":" + scriptJson + "}";
		gameScript = JsonHelper.FromJson<Line>(scriptJson);

		cam = Camera.main;
		sceneRunning = false;
		fadingIn = fadingOut = false;
		zoomingIn = zoomingOut = false;
		bubbleIndex = 0;
	}

	public void StartScene (string sceneTag) {


		
		bubbleIndex = GetSceneStartIndex(sceneTag);
		print ("bubbleIndex is" + bubbleIndex);

		sceneRunning = true;

		StartCoroutine("NextWindow", true);

		ZoomIn();
	}

	public void EndScene() {
		sceneRunning = false;

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
		int textLines = gameScript [bubbleIndex].text.Split ('\n').Length;

		Vector3 offset = new Vector3(0, 175 + (50*textLines), 0);

		string speakerName = gameScript [bubbleIndex].speaker;
		GameObject speaker = GameObject.Find (speakerName);
		Transform bubble = SelectBubble (speaker);
		activeBubble = Instantiate(bubble, speaker.transform.position + offset, Quaternion.identity);

		// Convert from world position to canvas position.
		RectTransform CanvasRect = activeBubble.GetComponent<RectTransform>();
		Vector3 pos = speaker.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint (pos);
		CanvasRect.anchorMin = viewportPoint;
		CanvasRect.anchorMax = viewportPoint;

		activeBubble.gameObject.GetComponentInChildren<Image> ().material.color = white;

		Text bubbleText = activeBubble.GetComponentInChildren<Text>();
		bubbleText.text = gameScript[index].text;
		activeBubble.SetParent (canv.transform, false);
		activeBubble.gameObject.SetActive (true);

		activeBubble.gameObject.GetComponentInChildren<Image> ().material.color = transparent;
		fadingIn = true;
		StartCoroutine ("StopFadeIn");
	}

	void RemoveText() {
		fadingOut = true;
		StartCoroutine("NextWindow", false);

	}

	IEnumerator StopFadeIn() {
		yield return new WaitForSeconds (0.4f);
		fadingIn = false;
	}
		
	IEnumerator NextWindow(bool first) {
		// Display the next window, if there is one to display
		yield return new WaitForSeconds (0.4f);

		if (!first) {
			//activeBubble.gameObject.SetActive (false);
			Destroy(activeBubble.gameObject);
		}

		fadingOut = false;

		// Don't check the previous statement's end value if it's a first statement
		if (!first) {
			if (gameScript [bubbleIndex - 1].end) {
				EndScene ();
			} else {
				DisplayText (bubbleIndex);
			}
		} else {
			DisplayText (bubbleIndex);
		}
			
		bubbleIndex++;
	}

	private Vector3 _SpeakerPosition() {
		string nextSpeakerName = gameScript[bubbleIndex].speaker;
		// TODO Get them by tag instead?
		Vector3 nextLocation = GameObject.Find (nextSpeakerName).transform.position;
		return nextLocation;
	}

	private int GetSceneStartIndex(string sceneTag) {
		for (int i=0; i<gameScript.Length; i++) {
			if (gameScript[i].tag == sceneTag) {
				return i;
			}
		}
		return -1;
	}

	private Transform SelectBubble(GameObject speaker) {
		if (speaker.transform.position.x <= cam.transform.position.x) {
			return bubbleRight;
		} else {
			return bubbleLeft;
		}
	}
		
}
