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
        /*
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
        */
	}
    
    //*************************************************************//
	// State Machine
	//*************************************************************//
	protected void ChangeState (eAniState eState)
	{
		if (m_eAniState == eState) {
			return;
		}
        
        base.ChangeState (eState);

		m_eAniState = eState;
		switch (eState)
		{
        }
    }
    
	//*************************************************************//
	// Broadcast method
	//*************************************************************//
    protected override void OnEvent (eEvent msg, System.Object param1, string param2) 
    {
        switch (msg)
        {
            case eEvent.Start:
            	Event_GameStart();
                break;
            
           case eEvent.GameOver:
           		Event_GameOver();
                break;
                
           case eEvent.Ready:
                Event_Ready();
                break;
                
           case eEvent.StageClear:
                Event_StageClear();
                break;
        }
    }

    
    void Event_Ready()
    {
        ChangeState (eAniState.Ready);
    }
    
	void Event_GameStart()
	{
        //AddRigidbody();
		ChangeState (eAniState.None);
	}

	void Event_GameOver()
	{
		ChangeState (eAniState.Dead);
	}
    
    void Event_StageClear()
    {
        ChangeState (eAniState.Arrive_Goal);
    }
}
