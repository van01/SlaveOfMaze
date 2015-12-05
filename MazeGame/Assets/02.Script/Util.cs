using UnityEngine;
using System.Collections;

public class Util {

	static public GameObject GetPrefab(string prefabName, eResType eType = eResType.Default)
	{
		string path = string.Empty;
		
		switch (eType) {
		case eResType.Default:
			path = "Models/mmmm/core/prefabs/{0}";
			break;
		case eResType.Custom:
			path = "prefabs/{0}";
			break;
		case eResType.Popup:
			path = "prefabs/Popup/{0}";
			break;
		}
		
		string resPath = string.Format (path, prefabName);
		GameObject prefabs = Resources.Load ( resPath ) as GameObject;
		GameObject obj = GameObject.Instantiate( prefabs );
		return obj;
	}
}
