using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	// Needs a size
	public int xSize, zSize;

	//Needs reference to individual cell prefabs
	public MazeCell cellPrefab;
	private MazeCell[,] cells;

	public void Generate()
	{
		cells = new MazeCell[xSize, zSize];
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				//Create cell - instantiate, name it, parent it, position it
				CreateCell(x,z);
			}
		}
	}

	private void CreateCell(int x, int z)
	{
		MazeCell cellIns = Instantiate (cellPrefab) as MazeCell;
		cellIns.name = "Cell " + x + " " + z;
		cellIns.transform.parent = transform;
		cellIns.transform.position = new Vector3 (x - xSize * 0.5f + 0.5f, 0, z - zSize * 0.5f + 0.5f);

		cells [x, z] = cellIns;

	}
}
