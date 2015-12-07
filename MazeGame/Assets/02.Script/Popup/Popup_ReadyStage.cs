using UnityEngine;
using System.Collections;

public class Popup_ReadyStage : MonoBehaviour {

	void OnOK ()
	{
		GameManager.GetInstance ().OnStart ();
		gameObject.SetActive (false);
		Destroy (this);
	}
}
