using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void OnGoal() {
		PopupMgr.GetInstance().ShowPopup (ePopupType.StageEnd);
	}

	public void NextStage()
	{
		TileManager tileMgr = TileManager.GetInstnace ();

		m_nStage++;
		if (m_nStage > 2) {
			m_nStage = 1;
		}

		tileMgr.StartStage (m_nStage);
		Vector2 startPosition = tileMgr.GetStartPosition ();
		m_Player.Initialize (startPosition.x, startPosition.y);
		//m_Player.transform.position = new Vector3 (startPosition.x, 0, startPosition.y);

	}
}
