using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEditor : EditorWindow
{
	[MenuItem ("MapTools/MapEditor")]
	
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(MapEditor));
	}
	string myString = "test";
	bool groupEnabled;
	bool myBool;
	float myFloat;
	void OnGUI () {
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();

	}
}

