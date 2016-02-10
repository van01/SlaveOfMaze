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
    
    
    void SetAIFollw(bool isPlay)
    {
        AIFollow aiFollw = GetComponent<AIFollow>();
        if (aiFollw != null)
        {
            aiFollw.enabled = isPlay;
        }
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

		switch (eState)
		{
            case eAniState.None:
                SetAIFollw (true);
                break;
            case eAniState.Stop:
                SetAIFollw (false);
                break;
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
	}
    
    void Event_StageClear()
    {
        ChangeState (eAniState.Stop);
    }

}
