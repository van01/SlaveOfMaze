using UnityEngine;
using System.Collections;

public class PopupMgr : MonoBehaviour {

	public delegate void deleCallback(int nType);

	static PopupMgr m_instance = null;
	static public PopupMgr GetInstance()
	{
		if (m_instance == null) {
			m_instance = GameObject.Find ("GameManager").AddComponent<PopupMgr>();
		}

		return m_instance;
	}

	public void ShowPopup (ePopupType ePopup, deleCallback delgate, string strDesc, string strButton)
	{
		GameObject obj = null;
		switch (ePopup)
		{
		case ePopupType.Common:
		{
			obj = Util.GetPrefab ("Popup_Common", eResType.Popup);
			Popup_Common popup = obj.GetComponent <Popup_Common>();
			popup.SetDelegate (delgate);
			popup.SetText (strDesc, strButton);
		}
			break;
		}

		if (obj != null)
		{
			obj.transform.parent = this.transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
		}
	}
}
