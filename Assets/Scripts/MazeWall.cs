using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge {

	// A wall will be created
	// when we are exiting the grid
	// or the next cell already exists

	public Transform wall;

	public override void Initialise (MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		base.Initialise (_cell, _otherCell, _direction);
		transform.GetChild(0).GetComponent<Renderer> ().material = _cell.room.setting.wallMaterial;
	}


}
