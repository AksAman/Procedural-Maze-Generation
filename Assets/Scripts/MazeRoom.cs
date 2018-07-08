using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MazeRoom : ScriptableObject
{
	public List<MazeCell> cells = new List<MazeCell>();
	public MazeRoomSetting setting;
	public int settingIndex;

	public void Add(MazeCell _cell)
	{
		cells.Add (_cell);
		_cell.room = this;
	}

}

