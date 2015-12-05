using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	private GameObject m_objTileMap = null;
	private GameObject m_objBlockMap = null;

	private Vector2 m_StartPos;

	static TileManager pInstnace = null;

	public static TileManager GetInstnace()
	{
		if (pInstnace == null) 
		{
			pInstnace = FindObjectOfType<TileManager>();
		}
		return pInstnace;
	}

	public void ClearMap()
	{
		if (m_objTileMap != null) {
			Destroy (m_objTileMap);
		}
		m_objTileMap = new GameObject ();
		m_objTileMap.transform.parent = this.transform;

		if (m_objBlockMap != null) {
			Destroy (m_objBlockMap);
		}
		m_objBlockMap = new GameObject ();
		m_objBlockMap.transform.parent = this.transform;
	}

	public void StartStage(int nStageNumber)
	{
		ClearMap();

		int [,] arMap = MapData.GetInstance().GetTileMap (nStageNumber);
		int [,] arBlock = MapData.GetInstance().GetBlock (nStageNumber);
		int [,] arStartEnd = MapData.GetInstance().GetStartEnd  (nStageNumber);

		for (int i=0; i<arMap.GetLength(0); ++i) 
		{
			for (int j=0; j<arMap.GetLength(1); j++)
			{
				int nNum = arMap[i, j];
				GameObject obj = GetTile(nNum);
				if (obj != null)
				{
					obj.transform.parent = m_objTileMap.transform;
					obj.transform.localPosition = new Vector3 (i,0, j);
				}
				else
				{
					Debug.Log ("Tile is NULL");
				}
			}
		}

		for (int i=0; i<arBlock.GetLength(0); ++i) {
			for (int j=0; j<arBlock.GetLength(1); ++j)
			{
				int nNum = arBlock[i, j];
				if (nNum > 0)
				{
					GameObject obj = GetBlock ();
					if (obj != null)
					{
						obj.transform.parent = m_objBlockMap.transform;
						obj.transform.localPosition = new Vector3 (i, 0, j);
					}
					else
					{
						Debug.Log ("Block is null");
					}
				}
			}
		}

		for (int i=0; i<arStartEnd.GetLength(0); ++i) 
		{
			for (int j=0; j<arStartEnd.GetLength(1); j++)
			{
				int nNum = arStartEnd[i, j];
				if (nNum == 1)
				{
					m_StartPos = new Vector2 (i, j);
				}
				else if (nNum == 2)
				{
					Vector2 pos = new Vector2 (i, j);
					SetExit (pos);
				}
			}
		}

	}

	private void SetExit (Vector2 pos)
	{
		GameObject exitObject = Util.GetPrefab ("Goal", eResType.Custom);
		exitObject.transform.parent = m_objBlockMap.transform;
		exitObject.transform.localPosition = new Vector3 (pos.x, 0, pos.y);
	}

	private GameObject GetBlock()
	{
		string [] arString = {"obj_planter1"};
		//string strCharater = string.Empty;
		//int nSubNum = 2;
		int nLastNum = 0;
		
		string prefabName = arString[nLastNum];

		GameObject tileObject = Util.GetPrefab (prefabName);
		tileObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

		return tileObject;
	}


	private GameObject GetTile(int nNum)
	{
		string [] arString = {"crete","grass"};
		string [] arGrassString ={"a","b","c","d","e","f","g","h"};
		string [] arCreteString ={"a","b","c","d"};
		string strCharater = string.Empty;
		//int nNum = 1;//UnityEngine.Random.Range (0, 2);
		int nSubNum = 2;
		int nLastNum;

		if (nNum == 0) {
			nLastNum = UnityEngine.Random.Range (0, arCreteString.Length);
			strCharater = arCreteString[nLastNum];
		} else {
			nLastNum = UnityEngine.Random.Range (0, arGrassString.Length);
			strCharater = arGrassString[nLastNum];
		}

		string prefabName = string.Format ("env_{0}{1}{2}", arString[nNum], nSubNum, strCharater);
		string path = string.Format ("Models/mmmm/core/prefabs/{0}", prefabName);
//		Debug.Log ("path : " + path);
		GameObject prefabs = Resources.Load ( path ) as GameObject;
		GameObject tileObject = GameObject.Instantiate( prefabs );
		tileObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

		return tileObject;
	}

	public Vector2 GetStartPosition ()
	{
		return m_StartPos;
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
