using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int m_nStage = 1;
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
		GameObject.Find ("Popup_ClearStage").SetActive (true);
	}

	public void NextStage()
	{
		TileManager tileMgr = TileManager.GetInstnace ();
		
		tileMgr.StartStage (m_nStage);
		Vector2 startPosition = tileMgr.GetStartPosition ();
		m_Player.Initialize (startPosition.x, startPosition.y);
		//m_Player.transform.position = new Vector3 (startPosition.x, 0, startPosition.y);

	}
}
