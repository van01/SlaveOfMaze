using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private enum eStage
	{
		Stage_Ready,
		Stage_End,
		Stage_StartGame,
	}
	private eStage m_eStage;

	public int m_nStage = 0;
	public UnitControl m_Player;

	static GameManager m_instance = null;
	static public GameManager GetInstance()
	{
		if (m_instance == null) {
			m_instance = GameObject.Find ("GameManager").GetComponent<GameManager>();
		}
		return m_instance;
	}

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this);

		ChangeStage (eStage.Stage_Ready);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnReady() {
		ChangeStage (eStage.Stage_Ready);
	}
	public void OnStart() {
		ChangeStage (eStage.Stage_StartGame);
	}
	public void OnGoal() {
		ChangeStage (eStage.Stage_End);
	}

	public void StartGame(int nStage)
	{
		TileManager tileMgr = TileManager.GetInstnace ();

		tileMgr.StartStage (nStage);
		Vector2 startPosition = tileMgr.GetStartPosition ();
		m_Player.Initialize (startPosition.x, startPosition.y);
		//m_Player.transform.position = new Vector3 (startPosition.x, 0, startPosition.y);
	}

	public void NextStage()
	{
		m_nStage++;
		if (m_nStage > 2) {
			m_nStage = 1;
		}

		StartGame (m_nStage);
	}

	private void ChangeStage (eStage stage)
	{
		m_eStage = stage;

		switch (stage) {
		case eStage.Stage_Ready:
			PopupMgr.GetInstance().ShowPopup (ePopupType.StageReady);
			break;
		case eStage.Stage_End:
			PopupMgr.GetInstance().ShowPopup (ePopupType.StageEnd);
			break;
		case eStage.Stage_StartGame:
			StartGame(m_nStage);
			break;
		default:
			break;
		}
	}
}
