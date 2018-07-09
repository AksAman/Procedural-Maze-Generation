using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	// Needs a size
	public IntVector2 size;

	//Needs reference to individual cell prefabs
	public MazeCell cellPrefab;
	private MazeCell[,] cells;
	public GameObject ceilingPrefab;
	public MazeWall[] mazeWallPrefabs;
	public MazePassage mazePassPrefab;
	public MazeDoor mazeDoorPrefab;

	[Range(0,1)]
	public float doorProbability;


	private List<MazeRoom> rooms = new List<MazeRoom>();
	[Space]
	public MazeRoomSetting[] roomSettings = new MazeRoomSetting[4];
	public GameObject singlePrefab;

	public bool expandRooms;
	private enum IndexMethod
	{
		Oldest,
		Newest,
		Random,
		Middle
	}

	[Space]
	[SerializeField]
	private IndexMethod indexMethod = new IndexMethod();

	public void Generate()
	{
		cells = new MazeCell[size.x, size.z];

		// List to keep track of active cells
		List<MazeCell> activeCells = new List<MazeCell> ();

		// Creating First cell and adding it to the list
		DoFirstGenerationStep (activeCells);
		while(activeCells.Count > 0)
		{
//			yield return new WaitForSeconds (0.03f);
			DoNextGenerationStep (activeCells);
		}

		foreach (MazeRoom room in rooms) {
			if(room.roomSize == 1)
			{
				GameObject singleCube = Instantiate (singlePrefab, room.cells [0].transform.position, Quaternion.identity, room.cells [0].transform) as GameObject;
			}
		}

		GameObject ceiling = Instantiate (ceilingPrefab, this.transform);
		ceiling.transform.GetChild (0).localScale = new Vector3 (size.x / 10f, 1, size.z / 10f);
		ceiling.transform.position = this.transform.position;
	}

	void DoFirstGenerationStep(List<MazeCell> _activeCells)
	{
		MazeCell newCell = CreateCell (RandomCoordinates);
		newCell.SetRoom (CreateRoom (-1));
		_activeCells.Add (newCell);

	}

	void DoNextGenerationStep(List<MazeCell> _activeCells)
	{
		// newest cell index
		int currentIndex = ChooseIndex(_activeCells.Count);
		MazeCell currentCell = _activeCells [currentIndex];
		if(currentCell.isFullyInitialized())
		{
			_activeCells.RemoveAt (currentIndex);
			return;
		}
		IntVector2 currentCoords = currentCell.coordinates;
		mazeDirection nextDirection = currentCell.RandomUninitializedDirection;
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
			else if(currentCell.room == neighbourCell.room && expandRooms)
			{
				CreatePassageInSameRoom (currentCell, neighbourCell, nextDirection);
			}
			else
			{
				CreateWall (currentCell, neighbourCell, nextDirection);
			}
		}
		else
		{
			//Create wall
			CreateWall (currentCell, null, nextDirection);

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

	void CreateWall(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		
		MazeWall mazeWall = Instantiate (mazeWallPrefabs[Random.Range(0, mazeWallPrefabs.Length)]) as MazeWall;
		mazeWall.Initialise (_cell, _otherCell, _direction);

		if (_otherCell != null) {
			mazeWall = Instantiate (mazeWallPrefabs[Random.Range(0, mazeWallPrefabs.Length)]) as MazeWall;
			mazeWall.Initialise (_otherCell, _cell, _direction.GetOpposite ());
		}
	}

	void CreatePassageInSameRoom(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		MazePassage mazePassage = Instantiate (mazePassPrefab);
		mazePassage.Initialise (_cell, _otherCell, _direction);

		mazePassage = Instantiate (mazePassPrefab);
		mazePassage.Initialise (_otherCell, _cell, _direction.GetOpposite());

	}

	void CreatePassage(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		MazePassage prefabPass = Random.value < doorProbability ? mazeDoorPrefab : mazePassPrefab;

		MazePassage mazePassage = Instantiate (prefabPass) as MazePassage;
		mazePassage.Initialise (_cell, _otherCell, _direction);

		if(mazePassage is MazeDoor)
		{
			//Create a new room, excluding the last index
			_otherCell.SetRoom ((CreateRoom (_cell.room.settingIndex)));

		}
		else
		{
			//initialize with the ongoing room
			_otherCell.SetRoom (_cell.room);
		}

		mazePassage = Instantiate (prefabPass) as MazePassage;
		mazePassage.Initialise (_otherCell, _cell, _direction.GetOpposite());
	}

	private MazeRoom CreateRoom(int indexToExclude)
	{
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom> ();

		newRoom.settingIndex = Random.Range (0, roomSettings.Length);
		if(newRoom.settingIndex == indexToExclude)
		{
			newRoom.settingIndex = (newRoom.settingIndex + 1) % roomSettings.Length;
		}
		newRoom.setting = roomSettings [newRoom.settingIndex];
		rooms.Add (newRoom);
		return newRoom;

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

	private int ChooseIndex(int count)
	{
		switch (indexMethod) {
		case IndexMethod.Newest:
			return count - 1;
		case IndexMethod.Oldest:
			return 0;
		case IndexMethod.Random:
			return Random.Range(0, count-1);
		case IndexMethod.Middle:
			return (int)((count - 1 )/ 2);
		default:
			return count - 1;;
		}

	}
	#endregion
}
