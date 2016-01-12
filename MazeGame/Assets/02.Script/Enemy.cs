using UnityEngine;
using System.Collections;

public class Enemy : UnitControl {

	// Use this for initialization
	void Start () {
		base.Start (eType.Enermy);

		GetComponent<AIFollow> ().target = Player.GetInstance ().gameObject.transform;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	protected override void InputProcess()
	{
	}
}
