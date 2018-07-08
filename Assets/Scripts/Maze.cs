using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	// Needs a size
	public IntVector2 size;

	//Needs reference to individual cell prefabs
	public MazeCell cellPrefab;
	private MazeCell[,] cells;
	public MazeWall mazeWallPrefab;
	public MazePassage mazePassPrefab;

	public void Generate()
	{
		cells = new MazeCell[size.x, size.z];

		// List to keep track of active cells
		List<MazeCell> activeCells = new List<MazeCell> ();

		// Creating First cell and adding it to the list
		DoFirstGenerationStep (activeCells);
		while(activeCells.Count > 0)
		{
//			yield return new WaitForSeconds (0.05f);
			DoNextGenerationStep (activeCells);
		}
		Debug.Log (transform.childCount.ToString ());
	}

	void DoFirstGenerationStep(List<MazeCell> _activeCells)
	{
		_activeCells.Add (CreateCell (RandomCoordinates));
	}

	void DoNextGenerationStep(List<MazeCell> _activeCells)
	{
		int currentIndex = _activeCells.Count - 1;
		MazeCell currentCell = _activeCells [currentIndex];
		if(currentCell.isFullyInitialized())
		{
			_activeCells.RemoveAt (currentIndex);
			return;
		}

		IntVector2 currentCoords = currentCell.coordinates;
		MazeDirection nextDirection = currentCell.RandomUninitializedDirection;
		IntVector2 nextCellCoords = currentCoords + nextDirection.ToIntVector2 ();


		if(isCoordInRange(nextCellCoords))
		{
			MazeCell neighbourCell = GetCell (nextCellCoords);
			// Check if cell already or not 
			if(neighbourCell == null)
			{
				// i.e. cell doesn't exists
				neighbourCell = CreateCell (nextCellCoords);
				CreatePassage (currentCell, neighbourCell, nextDirection);
				_activeCells.Add (neighbourCell);
			}

			else
			{
				CreateWall (currentCell, neighbourCell, nextDirection);
//				_activeCells.RemoveAt (currentIndex);
			}
		}
		else
		{
			//Create wall
			CreateWall (currentCell, null, nextDirection);
//			_activeCells.RemoveAt (currentIndex);

		}
	}

	private MazeCell CreateCell(IntVector2 _coordinates)
	{
		MazeCell cellIns = Instantiate (cellPrefab) as MazeCell;
		cellIns.coordinates = _coordinates;
		cellIns.transform.parent = this.transform;
		cellIns.name = "Cell " + _coordinates.x + ", " + _coordinates.z;
		cellIns.transform.position = new Vector3 (_coordinates.x - size.x * 0.5f + 0.5f, 0, _coordinates.z - size.z * 0.5f + 0.5f);
		cells [_coordinates.x, _coordinates.z] = cellIns;

		return cellIns;
	}

	void CreateWall(MazeCell _cell, MazeCell _otherCell, MazeDirection _direction)
	{
		MazeWall mazeWall = Instantiate (mazeWallPrefab) as MazeWall;
		mazeWall.Initialise (_cell, _otherCell, _direction);

		if (_otherCell != null) {
			mazeWall = Instantiate (mazeWallPrefab) as MazeWall;
			mazeWall.Initialise (_otherCell, _cell, _direction.GetOpposite ());
		}
	}

	void CreatePassage(MazeCell _cell, MazeCell _otherCell, MazeDirection _direction)
	{
		MazePassage mazePassage = Instantiate (mazePassPrefab) as MazePassage;
		mazePassage.Initialise (_cell, _otherCell, _direction);

		mazePassage = Instantiate (mazePassPrefab) as MazePassage;
		mazePassage.Initialise (_otherCell, _cell, _direction.GetOpposite());
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

	public MazeCell GetCell(IntVector2 _coordinates)
	{
		return cells [_coordinates.x, _coordinates.z];
	}
	#endregion
}
