using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTileSet : MonoBehaviour
{

	/* Class: BuildTileSet
	 * 
	 * Purpose: This script will create the tileset used to spawn items and NPCs
	 * 
	 */

	//How to build tileset
	//Start with one prefab already placed
	//Build off of it recursively filling a 10x20 area
	//Use a recusrive loop
	//Print new prefab every 60m


	public GameObject tilePrefab;
	private int tileGridSizeX = 20, tileGridSizeY = 10;  //The current grid is 10x20
	private int prevTileLocX = 0, prevTileLocY = 0; //Use these to properly space out clones

    // Start is called before the first frame update
    void Start()
    {
		CreateGrid();
    }

	private void CreateGrid()
	{
		//First prefab is placed at 570, 270
		//this loop will fill the 20x10 area of the map with the Tile prefab
		for (int i = 0; i < tileGridSizeY; i++)
		{
			if( i != 0)
			{
				prevTileLocY -= 60;
			}

			for (int j = 0; j < tileGridSizeX; j++)
			{
				if(i == 0 && j == 0)
				{
					prevTileLocX = -570;
					prevTileLocY = 270;
					GameObject firstTileClone = Instantiate(tilePrefab, new Vector3(-570, 270, 0), Quaternion.identity);
					firstTileClone.transform.SetParent(GameObject.FindGameObjectWithTag("tiles").transform, false);
				}
				if(i != 0 && j == 0)
				{
					prevTileLocX = -570;
				}

				GameObject tileClone = Instantiate(tilePrefab, new Vector3(prevTileLocX, prevTileLocY, 0), Quaternion.identity);
				tileClone.transform.SetParent(GameObject.FindGameObjectWithTag("tiles").transform, false);

				prevTileLocX += 60;
			}
		}
	}
}
