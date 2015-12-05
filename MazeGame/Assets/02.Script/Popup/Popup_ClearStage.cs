using UnityEngine;
using System.Collections;

public class Popup_ClearStage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnOK ()
	{
		GameManager.GetInstance ().NextStage ();
		gameObject.SetActive (false);
		Destroy (this);
	}
}
