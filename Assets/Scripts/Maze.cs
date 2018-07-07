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
		IntVector2 currentCoord = RandomCoordinates;
		while(isCoordInRange(currentCoord) && !doesCellExitsAt(currentCoord))
		{
			CreateCell (currentCoord);
			currentCoord += MazeDirections.RandomDirection().ToIntVector2();
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


	#region Helper functions
	//Property to generate random cell coordinate
	public IntVector2 RandomCoordinates 
	{
		get
		{
			return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
		}
	}

	//Checks if passed coordinate is within boundary
	public bool isCoordInRange(IntVector2 _coordinate)
	{
		return (_coordinate.x >= 0 && _coordinate.z >= 0 && _coordinate.x < size.x && _coordinate.z < size.z);
	}

	//Checks if there exists a cell at a given coordinate
	public bool doesCellExitsAt(IntVector2 _coordinate)
	{
		if (cells [_coordinate.x, _coordinate.z] == null) {
			return false;
		} else
			return true;
	}
	#endregion
}
