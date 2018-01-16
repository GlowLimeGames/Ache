using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	public Canvas canv;
	public Transform speakerlessText;

	public TextAsset script;

	public bool sceneRunning;
	private Transform activeBubble;
	private int bubbleIndex;

	//static string scriptPath = "Assets/_Dialogue/Level 1 Dialogue.txt";
	string scriptJson;
	Line[] gameScript;

	// Use this for initialization
	void Start () {
		//scriptJson = System.IO.File.ReadAllText (scriptPath);
		scriptJson = script.text;
		print (scriptJson);
		scriptJson = "{\"Items\":" + scriptJson + "}";
		gameScript = JsonHelper.FromJson<Line>(scriptJson);

		sceneRunning = false;
		bubbleIndex = 0;

		StartScene ("tutorial");
	}

	public void StartScene (string sceneTag) {

		bubbleIndex = GetSceneStartIndex(sceneTag);
		print ("bubbleIndex is" + bubbleIndex);

		sceneRunning = true;

		StartCoroutine("NextWindow", true);

	}

	public void EndScene() {
		sceneRunning = false;

	}

	// Update is called once per frame
	void Update () {
		
		// Check for input, advance text if the right tutorial thing has happened

		if (sceneRunning) {
			// Walking
			if ((bubbleIndex == 1) && (Input.GetKeyDown("d"))) {
				RemoveText ();
			}

			// Jumping
			if ((bubbleIndex == 2) && (Input.GetKeyDown ("w"))) {
				RemoveText ();
			}

			// Crawling (not implemented, but check the key anyway)
			if ((bubbleIndex == 3) && (Input.GetKeyDown ("s"))) {
				RemoveText ();
			}

			// Collecting the backpack is next
		}

	}

	void DisplayText(int index) {
		int textLines = gameScript [bubbleIndex].text.Split ('\n').Length;

		Transform bubble = speakerlessText;
		activeBubble = Instantiate(bubble, bubble.position, Quaternion.identity);

		// Convert from world position to canvas position.
		//RectTransform CanvasRect = activeBubble.GetComponent<RectTransform>();
		//Vector3 pos = speaker.transform.position;
		//Vector2 viewportPoint = Camera.main.WorldToViewportPoint (pos);
		//CanvasRect.anchorMin = viewportPoint;
		//CanvasRect.anchorMax = viewportPoint;

		//Text bubbleText = activeBubble.GetComponent<Text> ();
		//bubbleText.text = gameScript[index].text;
		Text bubbleText = activeBubble.GetComponent<Text>();
		bubbleText.text = gameScript[index].text;
		activeBubble.SetParent (canv.transform, false);
		activeBubble.gameObject.SetActive (true);

		//activeBubble.gameObject.GetComponentInChildren<Image> ().material.color = transparent;
		//fadingIn = true;
		//StartCoroutine ("StopFadeIn");
	}

	void RemoveText() {
		StartCoroutine("NextWindow", false);

	}

	IEnumerator NextWindow(bool first) {
		// Display the next window, if there is one to display

		// Longer wait for tutorial windows
		yield return new WaitForSeconds (0.7f);

		if (!first) {
			//activeBubble.gameObject.SetActive (false);
			Destroy(activeBubble.gameObject);
		}

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


	private int GetSceneStartIndex(string sceneTag) {
		for (int i=0; i<gameScript.Length; i++) {
			if (gameScript[i].tag == sceneTag) {
				return i;
			}
		}
		return -1;
	}
}
