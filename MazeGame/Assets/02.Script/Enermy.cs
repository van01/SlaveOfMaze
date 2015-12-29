using UnityEngine;
using System.Collections;

public class Enermy : UnitControl {

	// Use this for initialization
	void Start () {
		base.Start (eType.Enermy);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	protected override void InputProcess()
	{
	}
}
