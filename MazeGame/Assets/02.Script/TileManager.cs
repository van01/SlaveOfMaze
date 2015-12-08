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
		MapFile fileMap = MapData.GetInstance().GetTileMap (nStageNumber);
		CreateMap (fileMap);
	}

	private void CreateMap (MapFile fileMap)
	{
		for (int i=0; i<fileMap.GetHeight(); ++i) {
			for (int j=0; j<fileMap.GetWidth(); ++j)
			{
				TileData tile = fileMap.GetTile (i,j);
				//***************************************//
				
				GameObject objTile = GetTile(tile.nTile);
				if (objTile != null)
				{
					objTile.transform.parent = m_objTileMap.transform;
					objTile.transform.localPosition = new Vector3 (i,0, j);
				}
				else
				{
					Debug.Log ("Tile is NULL");
				}
				
				//***************************************//
				if (tile.nBlock > 0)
				{
					GameObject objBlock = GetBlock ();
					if (objBlock != null)
					{
						objBlock.transform.parent = m_objBlockMap.transform;
						objBlock.transform.localPosition = new Vector3 (i, 0, j);
					}
					else
					{
						Debug.Log ("Block is null");
					}
				}
				
				//***************************************//
				if (tile.nEvent == 1)
				{
					m_StartPos = new Vector2 (i, j);
				}
				else if (tile.nEvent == 2)
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

	public void CreateNewMap (int nWidth, int nHeight, int nTileType)
	{
		ClearMap();
		MapData.GetInstance().CreateNewMap (nWidth, nHeight, nTileType);
		MapFile fileMap = MapData.GetInstance ().GetEditMap ();
		CreateMap (fileMap);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
