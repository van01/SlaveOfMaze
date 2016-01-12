using UnityEngine;
using System.Collections;

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
			m_instance = GameObject.Find ("GameManager").GetComponent<GameManager>();
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
		Player.GetInstance().Event_GameStart();

		m_AstarPath.Scan ();
	}
	private void OnReady() {
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbStart, "Ready", "GO");

		try {
			Player.GetInstance().Event_Ready();
		}catch (System.Exception e)
		{
			Debug.LogWarning (e.ToString());
		}
	}
	private void OnStageClear() {
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbNextStage, "Stage Clear", "Complete");
	}
	private void OnGameOver()
	{
		PopupMgr.GetInstance().ShowPopup (ePopupType.Common, cbGameOver, "Game Over", "Retry");
		Player.GetInstance().Event_GameOver();
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
		case eStage.Stage_Clear:
			OnStageClear();
			break;
		case eStage.Stage_StartGame:
			OnStart ();
			break;
		case eStage.Stage_GameOver:
			OnGameOver();
			break;
		default:
			break;
		}
	}

}
