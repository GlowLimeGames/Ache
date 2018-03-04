using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public int SceneToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		SceneManager.LoadScene (SceneToLoad);

        
        if (SceneToLoad == 3)
        {
            Destroy(Dialogue.Instance);
            Destroy(GameManager.Instance.transform.GetChild(0).gameObject);

            AudioController.Instance.StopMusic();
            AudioController.Instance.PlayMusic("forboding");
            AudioController.Instance.PlayMusic("ambiance 2");
        }else if (SceneToLoad == 5)
        {
            AudioController.Instance.PlayLoop("many crows loop");
        }else if (SceneToLoad == 7)
        {
            AudioController.Instance.StopMusic();
            AudioController.Instance.PlayMusic("gyggas");
        }
	}
}
