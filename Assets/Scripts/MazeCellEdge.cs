using UnityEngine;
using System.Collections;

public abstract class MazeCellEdge : MonoBehaviour
{
	// An edge basically connects two cells, so
	// every edge should know its cell and the other cell it's
	// connected to. Also every edge is oriented to specific direction

	public MazeCell cell, otherCell;
	public mazeDirection direction;

	public virtual void Initialise(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		this.cell = _cell;
		this.otherCell = _otherCell;
		this.direction = _direction;
		transform.parent = _cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = _direction.ToRotation ();

		//Cell should know about the edge too, Smart edge yayy
		_cell.SetEdge (this, _direction);
	}
		
}

