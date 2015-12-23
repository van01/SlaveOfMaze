using UnityEditor;
using UnityEngine;

public class Edit_Popup_CreateMap : EditorWindow {

	static EditorWindow window = null;
	int nWidth = 100;
	int nHeight = 100;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("MapTools/CreateMap")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		if (window == null) {
			window = EditorWindow.GetWindow (typeof(Edit_Popup_CreateMap));
		} else {
			window.Show();
		}
	}
	
	void OnGUI()
	{
		GUILayout.Label ("New Map", EditorStyles.boldLabel);
		nWidth = EditorGUILayout.IntSlider (nWidth, 1, 1000);
		nHeight = EditorGUILayout.IntSlider (nHeight, 1, 1000);


		if(GUILayout.Button("Build Object"))
		{
			TileManager.GetInstnace().CreateNewMap (nWidth, nHeight, 0);
			window.Close();
		}
	}
}
