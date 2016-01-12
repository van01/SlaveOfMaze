using UnityEngine;
using System.Collections;

public class Player : UnitControl {

	static Player m_Instnace = null;

	static public Player GetInstance ()
	{
		if (m_Instnace == null) {
			m_Instnace = GameObject.Find ("Player").GetComponent<Player>();
		}
		return m_Instnace;
	}

	void OnEnable()
	{
		m_Instnace = this;
	}

	// Use this for initialization
	void Start () {
		base.Start (eType.Player);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	protected override void InputProcess()
	{
		//if (eAniState.Arrive_Goal != m_eAniState) {
		if (Input.GetKey ("up")) {
			//Move (0,1);
			ChangeState (eAniState.Move_Up);
		} else if (Input.GetKey ("down")) {
			//Move (0,-1);
			ChangeState (eAniState.Move_Down);
		} else if (Input.GetKey ("left")) {
			//Move (-1,0);
			ChangeState (eAniState.Move_Left);
		} else if (Input.GetKey ("right")) {
			//Move (1,0);
			ChangeState (eAniState.Move_Right);
		} else {
			ChangeState (eAniState.None);
		}
	}

}
