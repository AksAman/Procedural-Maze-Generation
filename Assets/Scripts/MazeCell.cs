﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
	public int initializedEdges;

	// The room this cell belongs to
	public MazeRoom room;

	public void SetRoom(MazeRoom _room)
	{
		_room.Add (this);
		this.transform.GetChild (0).GetComponent<Renderer> ().material = _room.setting.floorMaterial;
		this.transform.tag = _room.setting.RoomType;
	}

	public void SetEdge(MazeCellEdge _edge, mazeDirection _direction)
	{
		edges[(int)_direction] = _edge;
		initializedEdges++;
	}

	public MazeCellEdge GetEdge(mazeDirection _direction)
	{
		return edges [(int)_direction];
	}

	public bool isFullyInitialized()
	{
		return initializedEdges == 4;
	}

	public mazeDirection RandomUninitializedDirection
	{
		get{
			int skips = Random.Range (0, 4 - initializedEdges);

			for (int i = 0; i < 4; i++) {
				if (edges [i] == null) {
					if (skips == 0) {
						return (mazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException ("MazeCell has no uninitialized direction left");
		}
	}
}
