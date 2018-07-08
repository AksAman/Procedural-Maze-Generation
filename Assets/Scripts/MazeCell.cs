using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.count];

	public void SetEdge(MazeCellEdge _edge, MazeDirection _direction)
	{
		edges[(int)_direction] = _edge;
	}

	public MazeCellEdge GetEdge(MazeDirection _direction)
	{
		return edges [(int)_direction];
	}
}
