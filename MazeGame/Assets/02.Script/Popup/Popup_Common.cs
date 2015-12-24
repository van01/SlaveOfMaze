using UnityEngine;
using System.Collections;

public class Popup_Common : MonoBehaviour {

	public UILabel[] m_StaticLabel;

	PopupMgr.deleCallback m_delegate = null;

	public void SetDelegate(PopupMgr.deleCallback cbOK)
	{
		m_delegate = cbOK;
	}

	public void SetText(string strDesc, string strButton)
	{
		m_StaticLabel [0].text = strDesc;
		m_StaticLabel [1].text = strButton;
	}

	void OnOK ()
	{
		m_delegate (1);
		gameObject.SetActive (false);
		Destroy (this);
	}
}
