using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	// Needs a size
	public IntVector2 size;

	//Needs reference to individual cell prefabs
	public MazeCell cellPrefab;
	private MazeCell[,] cells;

	public void Generate()
	{
		cells = new MazeCell[size.x, size.z];
		for (int x = 0; x < size.x; x++) {
			for (int z = 0; z < size.z; z++) {
				//Create cell - instantiate, name it, parent it, position it
				CreateCell(new IntVector2(x,z));
			}
		}
	}

	private void CreateCell(IntVector2 _coordinates)
	{
		MazeCell cellIns = Instantiate (cellPrefab) as MazeCell;
		cellIns.coordinates = _coordinates;
		cellIns.transform.parent = this.transform;
		cellIns.name = "Cell " + _coordinates.x + " " + _coordinates.z;
		cellIns.transform.position = new Vector3 (_coordinates.x - size.x * 0.5f + 0.5f, 0, _coordinates.z - size.z * 0.5f + 0.5f);
		cells [_coordinates.x, _coordinates.z] = cellIns;
	}

	//Property to generate random cell coordinate
	public IntVector2 RandomCoordinates 
	{
		get
		{
			return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
		}
	}

	public bool isCoordInRange(IntVector2 _coordinate)
	{
		return (_coordinate.x >= 0 && _coordinate.z >= 0 && _coordinate.x < size.x && _coordinate.z < size.z);
	}
}
