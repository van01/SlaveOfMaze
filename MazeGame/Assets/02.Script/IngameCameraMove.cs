using UnityEngine;
using System.Collections;

public class IngameCameraMove : MonoBehaviour {

	GameObject m_targetObj;
	Vector3 m_basePosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_targetObj == null) 
		{
			GameObject ctrlObject = Player.GetInstance().gameObject;
			if (ctrlObject != null)
			{
				m_targetObj = ctrlObject;
				m_basePosition = new Vector3 (0, 5.27f, -2.84f);

				transform.position = m_basePosition + m_targetObj.transform.position;
				transform.LookAt (m_targetObj.transform.position);
			}
		}

		if (m_targetObj != null) 
		{
			transform.position = m_basePosition + m_targetObj.transform.position;
		}
	}
}
