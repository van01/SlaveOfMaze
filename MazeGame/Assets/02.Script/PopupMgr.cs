using UnityEngine;
using System.Collections;

public class PopupMgr : MonoBehaviour {

	static PopupMgr m_instance = null;

	static public PopupMgr GetInstance()
	{
		if (m_instance == null) {
			m_instance = GameObject.Find ("Panel").GetComponent<PopupMgr> ();
		}

		return m_instance;
	}

	public void ShowPopup (ePopupType ePopup)
	{
		GameObject obj = null;
		switch (ePopup)
		{
		case ePopupType.StageReady:
			obj = Util.GetPrefab ("Popup_ReadyStage", eResType.Popup);
			break;
		case ePopupType.StageEnd:
			obj = Util.GetPrefab ("Popup_ClearStage", eResType.Popup);
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
