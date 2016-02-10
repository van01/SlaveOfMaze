using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

	public int m_nMaxStage = 4;
	public int m_nStage = 1;
	

	private enum eStage
	{
		Stage_Ready,
		Stage_Clear,
		Stage_StartGame,
		Stage_GameOver,
	}
	private eStage m_eStage;

	static GameManager m_instance = null;
	static public GameManager GetInstance()
	{
		if (m_instance == null) {
            GameObject obj = GameObject.Find ("GameManager");
            if (obj != null)
            {
			     m_instance = obj.GetComponent<GameManager>();
            }
		}
		return m_instance;
	}

	public AstarPath m_AstarPath = null;
	string m_strMapName = string.Empty;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this);

		LoadStage (m_nStage);
		ChangeStage (eStage.Stage_Ready);
	}
	
	// Update is called once per frame
	void Update () {
	}


	
	//*************************************************************//
	// ChangeState Method
	//*************************************************************//
	private void OnStart()
	{
        m_AstarPath.Scan ();
        BroadcastMessage (eEvent.Start);
	}
	private void OnReady() {
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbStart, "Ready", "GO");
        BroadcastMessage (eEvent.Ready);
	}
	private void OnStageClear() {
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbNextStage, "Stage Clear", "Complete");
        BroadcastMessage (eEvent.StageClear);
	}
	private void OnGameOver()
	{
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbGameOver, "Game Over", "Retry");
        BroadcastMessage (eEvent.GameOver);
	}

	//*************************************************************//
	// Popup Callback Method
	//*************************************************************//
	public void cbStart(int nType) {
		ChangeStage (eStage.Stage_StartGame);
	}
	
	public void cbNextStage(int nType)
	{
		m_nStage++;
		if (m_nStage > m_nMaxStage) {
			m_nStage = 1;
		}

		LoadStage (m_nStage);
		ChangeStage (eStage.Stage_Ready);
	}
	
	public void cbGameOver(int nType)
	{
		LoadStage (m_nStage);
		ChangeStage (eStage.Stage_Ready);
	}

	//*************************************************************//
	// Event Listener method
	//*************************************************************//
    
    Action<eEvent, System.Object, string> m_listener;
    static public void AddListener (Action<eEvent, System.Object, string> cb)
    {
        GetInstance().m_listener += cb;
    }
    
    static public void RemoveListener(Action<eEvent, System.Object, string> cb)
    {
        GetInstance().m_listener -= cb;
    } 
    
    private void BroadcastMessage(eEvent ev, System.Object obj=null, string msg = "")
    {
        if (m_listener != null)
        {
            m_listener (ev, obj, msg);
        }
    }

	//*************************************************************//
	// Public method
	//*************************************************************//
	public void Event_Goal() {
		ChangeStage (eStage.Stage_Clear);
	}
	public void Event_GameOver() {
		ChangeStage (eStage.Stage_GameOver);
	}



	public void LoadStage(int nStage)
	{
		string strStageName = string.Format ("Stage{0:D2}", nStage);
		Debug.Log ("Load Level : " + strStageName);

		if (m_strMapName != string.Empty)
		{
			GameObject mapObj = GameObject.Find(m_strMapName);
			Destroy (mapObj);
		}

		m_strMapName = strStageName;
		uteMapLoader loader = gameObject.GetComponent<uteMapLoader> ();
		loader.SetMap (strStageName);
		loader.LoadMap ();


		IngameCameraMove cameraMove = Camera.main.GetComponent<IngameCameraMove> ();
		if (cameraMove == null) {
			cameraMove = Camera.main.gameObject.AddComponent <IngameCameraMove>();
		}
	}
	
	private void ChangeStage (eStage stage)
	{
		m_eStage = stage;

		switch (stage) {
		case eStage.Stage_Ready:
			OnReady ();
			break;
		case eStage.Stage_StartGame:
			OnStart ();
			break;
		case eStage.Stage_Clear:
			OnStageClear();
			break;
		case eStage.Stage_GameOver:
			OnGameOver();
			break;
		default:
			break;
		}
	}

}
