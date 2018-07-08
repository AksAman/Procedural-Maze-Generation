using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.count];
	public int initializedEdges;

	public void SetEdge(MazeCellEdge _edge, MazeDirection _direction)
	{
		edges[(int)_direction] = _edge;
		initializedEdges++;
	}

	public MazeCellEdge GetEdge(MazeDirection _direction)
	{
		return edges [(int)_direction];
	}

	public bool isFullyInitialized()
	{
		return initializedEdges == 4;
	}

	public MazeDirection RandomUninitializedDirection
	{
		get{
			int skips = Random.Range (0, 4 - initializedEdges);

			for (int i = 0; i < 4; i++) {
				if (edges [i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException ("MazeCell has no uninitialized direction left");
		}
	}
}
