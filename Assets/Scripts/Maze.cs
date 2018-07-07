using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	// Needs a size
	public IntVector2 size;

	//Needs reference to individual cell prefabs
	public MazeCell cellPrefab;
	private MazeCell[,] cells;

	public IEnumerator Generate()
	{
		cells = new MazeCell[size.x, size.z];

		// List to keep track of active cells
		List<MazeCell> activeCells = new List<MazeCell> ();

		// Creating First cell and adding it to the list
		DoFirstGenerationStep (activeCells);

		while(activeCells.Count > 0)
		{
			yield return new WaitForSeconds (0.05f);
			DoNextGenerationStep (activeCells);
		}
	}

	void DoFirstGenerationStep(List<MazeCell> _activeCells)
	{
		_activeCells.Add (CreateCell (RandomCoordinates));
	}

	void DoNextGenerationStep(List<MazeCell> _activeCells)
	{
		int currentIndex = _activeCells.Count - 1;
		MazeCell currentCell = _activeCells [currentIndex];
		IntVector2 currentCoords = currentCell.coordinates;
		IntVector2 nextCellCoords = currentCoords + MazeDirections.RandomDirection ().ToIntVector2 ();

		if(isCoordInRange(nextCellCoords) && !doesCellExitsAt(nextCellCoords))
		{
			_activeCells.Add (CreateCell (nextCellCoords));
		}
		else
		{
			_activeCells.RemoveAt (currentIndex);
		}
	}

	private MazeCell CreateCell(IntVector2 _coordinates)
	{
		MazeCell cellIns = Instantiate (cellPrefab) as MazeCell;
		cellIns.coordinates = _coordinates;
		cellIns.transform.parent = this.transform;
		cellIns.name = "Cell " + _coordinates.x + " " + _coordinates.z;
		cellIns.transform.position = new Vector3 (_coordinates.x - size.x * 0.5f + 0.5f, 0, _coordinates.z - size.z * 0.5f + 0.5f);
		cells [_coordinates.x, _coordinates.z] = cellIns;

		return cellIns;
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
