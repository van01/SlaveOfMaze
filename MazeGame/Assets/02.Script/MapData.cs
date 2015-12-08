using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileData{
	public int nTile;
	public int nBlock;
	public int nEvent;
	
	public TileData (int nTile, int nBlock=0, int nEvent = 0)
	{
		this.nTile = nTile;
		this.nBlock = nBlock;
		this.nEvent = nEvent;
	}
}

public class MapFile {
	TileData [,]m_Map;
	
	public MapFile (int nWidth, int nHeight, int nTileType)
	{
		m_Map = new TileData[nWidth, nHeight];
		
		for (int i=0; i<nWidth; i++)
		{
			for (int j=0; j<nHeight; j++)
			{
				m_Map[i,j] = new TileData(nTileType);
			}
		}
	}

	
	public MapFile (int [,]arMap, int [,]arBlock, int [,]arStartEnd)
	{
		int nHeight = arMap.GetLength (0);
		int nWidth = arMap.GetLength (1);

		m_Map = new TileData[nWidth, nHeight];
		
		for (int i=0; i<nHeight; i++) 
		{
			for (int j=0; j<nWidth; j++)
			{
				int nMap = arMap[i,j];
				int nBlock = arBlock[i,j];
				int nStartEnd = arStartEnd[i, j];
				
				m_Map[i,j] = new TileData(nMap, nBlock, nStartEnd);
			}
		}
	}

	public int GetWidth()
	{
		return m_Map.GetLength (0);
	}

	public int GetHeight()
	{
		return m_Map.GetLength (1);
	}

	public TileData GetTile (int i, int j)
	{
		return m_Map [i, j];
	}

}

public class MapData {

	static private MapData m_instnace = null;
	static public MapData GetInstance()
	{
		if (m_instnace == null) {
			m_instnace = new MapData();
		}
		return m_instnace;
	}

	//MapFile
	List<MapFile> m_ListMapContainer = new List<MapFile>();

	public MapData()
	{
		//arMap1;
		//arBlock1;
		//arStartEnd1

		MapFile mapFile = new MapFile (arMap1, arBlock1, arStartEnd1);
		m_ListMapContainer.Add (mapFile);

		mapFile = new MapFile (arMap2, arBlock2, arStartEnd2);
		m_ListMapContainer.Add (mapFile);

	}

	public MapFile GetTileMap(int nStage)
	{
		return m_ListMapContainer [nStage];
	}
	//******************************//
	// Edit Map
	//******************************//
	MapFile m_EditMap = null;
	public void CreateNewMap(int nWidth, int nHeight, int nTileType)
	{
		m_EditMap = new MapFile (nWidth, nHeight, nTileType);
	}

	public MapFile GetEditMap ()
	{
		return m_EditMap;
	}
	


	/*********** Stage 1 *************/
	//Tail map
	static int[,]arMap1 = {
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
	};
	
	//Block
	static int[,]arBlock1 = {
		{1,1,1,1,1,1,1,1,1,1},
		{0,0,1,1,1,1,1,1,1,1},
		{1,0,1,1,1,1,1,1,1,1},
		{1,0,0,0,0,1,1,1,1,1},
		{1,1,1,1,0,0,0,1,1,1},
		{1,1,1,1,1,1,0,1,1,1},
		{1,1,1,1,1,1,0,1,1,1},
		{1,1,1,1,1,1,0,1,1,1},
		{1,1,1,1,1,1,0,0,0,1},
		{1,1,1,1,1,1,1,1,0,1}
	};
	
	//StartEnd
	static int[,]arStartEnd1 = {
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,1,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,2,0},
	};

	/*********** Stage 2 *************/
	//Tail map
	static int[,]arMap2 = {
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
	};
	
	//Block
	static int[,]arBlock2 = {
		{1,1,1,1,1,1,1,1,1,1},
		{0,0,1,1,1,1,0,0,0,1},
		{1,0,0,0,0,1,0,1,0,1},
		{1,0,0,0,0,1,0,1,0,1},
		{1,0,0,0,0,1,0,1,0,1},
		{1,1,1,0,0,1,0,1,0,1},
		{1,1,1,0,0,1,0,1,0,1},
		{1,1,1,0,0,1,0,1,0,1},
		{1,1,1,0,0,0,0,1,0,1},
		{1,1,1,1,1,1,1,1,0,1}
	};
	
	//StartEnd
	static int[,]arStartEnd2 = {
		{0,0,0,0,0,0,0,0,0,0},
		{2,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,1,0},
	};
}
