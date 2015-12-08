using UnityEngine;
using System.Collections;

public class Edit_Popup_Create : MonoBehaviour {

	public UILabel m_LblWidth;
	public UILabel m_LblHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnOK()
	{
		int nWidth = int.Parse (m_LblWidth.text);
		int nHeight = int.Parse (m_LblHeight.text);
		TileManager.GetInstnace ().CreateNewMap (nWidth, nHeight, 0);

		this.gameObject.SetActive (false);
		Destroy (this);
	}
}
