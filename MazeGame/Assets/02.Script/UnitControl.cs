using UnityEngine;

public abstract class UnitControl : MonoBehaviour {

	public enum eType{
		Player,
		Enermy
	}

	protected eType m_eType = eType.Player;
	static float DEFAULT_SPEED = 5.0f;
	private Vector3 m_Position;
	private Vector3 m_PrevPosition;

	protected enum eAniState {
		None = 0,
		Move_Up = 1,
		Move_Down = 2,
		Move_Left = 3,
		Move_Right = 4,
		Arrive_Goal = 5,
		Fall_Out = 6,
		Dead = 7,
		Ready = 8,
        Stop = 9,
	};

	protected eAniState m_eAniState = eAniState.None;
    
	bool m_isControl = false;

	void OnEnable()
	{

	}
    
    void Awake()
    {
        GameManager.AddListener (OnEvent);
    }
    
    void OnDestroy()
    {
        GameManager.RemoveListener (OnEvent);
    }
    
    protected virtual void OnEvent (eEvent msg, System.Object param1, string param2) 
    {
        Debug.Log ("OnEvent : " + msg);  
        
        switch (msg)
        {
            case eEvent.Ready:
                ChangeState (eAniState.Ready);
                break;
            case eEvent.Start:
                ChangeState (eAniState.None);
                break;
            case eEvent.StageClear:
                ChangeState (eAniState.Arrive_Goal);
                break;
            case eEvent.GameOver:
                ChangeState (eAniState.Dead);
                break;
        }  
    }

	Rigidbody m_rigibody;

	// Use this for initialization
	protected void Start (eType type) {
		m_eType = type;
	}
	
	// Update is called once per frame
	protected void Update () {
		if (m_isControl) {
			InputProcess ();
		}

		ProcessState ();
		CheckState ();
	}

	protected abstract void InputProcess ();
    
    protected void AddRigidbody()
    {
        m_rigibody = gameObject.AddComponent<Rigidbody> ();
        m_rigibody.useGravity = true;
        m_rigibody.freezeRotation = true;
    }
	
	void Move()
	{
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

	void CheckState ()
	{
		switch (m_eAniState)
		{
		case eAniState.Dead:
			break;
		default:
			CheckFallOut();
			break;
		}
	}

	void CheckFallOut()
	{
		if (transform.position.y < -5.0f) {
			ChangeState (eAniState.Fall_Out);
		}
	}

	
	//*************************************************************//
	// Change Stage method
	//*************************************************************//
	protected void ChangeState (eAniState eState)
	{
		if (m_eAniState == eState) {
			return;
		}

		Debug.Log ("Change Ani : " + m_eAniState + " >> " + eState);
		m_eAniState = eState;
		switch (eState)
		{
		case eAniState.Ready:
			m_isControl = false;
			break;
		case eAniState.None:
			m_isControl = true;
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
			m_isControl = false;
			break;
		case eAniState.Fall_Out:
			m_isControl = false;
			if (m_eType == eType.Player)
			{
				GameManager.GetInstance().Event_GameOver();
			}
			break;
		case eAniState.Dead:
			m_isControl = false;
			m_rigibody.useGravity = false;
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
			Debug.Log ("Goal!!!!");
			if (m_eType == eType.Player)
			{
				GameManager.GetInstance().Event_Goal();
			}

		}
	}

	public void Initialize(float x, float z)
	{
		ChangeState (eAniState.None);
		gameObject.transform.position = new Vector3 (x, 0,z);
	}
}
