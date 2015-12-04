using UnityEngine;
using System.Collections;

public class UnitControl : MonoBehaviour {

	static float DEFAULT_SPEED = 5.0f;
	private Vector3 m_Position;
	private Vector3 m_PrevPosition;
	
	enum eAniState {
		None = 0,
		Move_Up = 1,
		Move_Down = 2,
		Move_Left = 3,
		Move_Right = 4,
		Arrive_Goal = 5,
	};

	eAniState m_eAniState = eAniState.None;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		InputProcess ();
		ProcessState ();
	}
	
	void Move()
	{
		bool isArrive = false;
		Vector3 pos = this.transform.localPosition;
		float fSpeed = (DEFAULT_SPEED * Time.deltaTime);


		switch (m_eAniState) {
		case eAniState.Move_Up:
			pos.z += fSpeed;
			break;
		case eAniState.Move_Down:
			pos.z -= fSpeed;
			break;
		case eAniState.Move_Left:
			pos.x -= fSpeed;
			break;
		case eAniState.Move_Right:
			pos.x += fSpeed;
			break;
		}
		this.transform.localPosition = pos;
	}

	void InputProcess()
	{
		if (eAniState.Arrive_Goal != m_eAniState) {
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

	void ProcessState ()
	{
		switch (m_eAniState) {
		case eAniState.Move_Up:
		case eAniState.Move_Down:
		case eAniState.Move_Left:
		case eAniState.Move_Right:
			Move ();
			break;
		}
	}

	void ChangeState (eAniState eState)
	{
		if (m_eAniState == eState) {
			return;
		}

		Debug.Log ("Change Ani : " + m_eAniState + " >> " + eState);
		m_eAniState = eState;
		switch (eState)
		{
		case eAniState.None:
			break;
		case eAniState.Move_Up:
			Move (0,1);
			break;
		case eAniState.Move_Down:
			Move (0,-1);
			break;
		case eAniState.Move_Left:
			Move (-1,0);
			break;
		case eAniState.Move_Right:
			Move (1,0);
			break;
		case eAniState.Arrive_Goal:
			GameManager.GetInstance().OnGoal();
			break;
		}
	}


	public void Move (int x, int z)
	{
		m_PrevPosition = m_Position = this.transform.localPosition;
		m_Position.x += x;
		m_Position.z += z;

		Vector3 LookAtPos = new Vector3 (x, 0, z);
		Quaternion direction =  gameObject.transform.localRotation;
		direction.SetLookRotation(LookAtPos);
		gameObject.transform.localRotation = direction;
	}

	void OnTriggerEnter(Collider other) {

		Debug.Log ("OnTriggerEnter : " + other.tag);

		if (other.tag == "Goal") {
			ChangeState (eAniState.Arrive_Goal);
			Debug.Log ("Goal!!!!");
		}
	}

	public void Initialize(float x, float z)
	{
		ChangeState (eAniState.None);
		gameObject.transform.position = new Vector3 (x, 0,z);
	}
}
