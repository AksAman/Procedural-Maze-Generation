using UnityEngine;

public class MazeDoor : MazePassage {

	public Transform hinge;

	private MazeDoor OtherSideDoor{
		get
		{
			return otherCell.GetEdge (direction.GetOpposite()) as MazeDoor;
		}
	}

	public override void Initialise(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		base.Initialise (_cell, _otherCell, _direction);

		if(OtherSideDoor != null)
		{
			hinge.localScale = new Vector3 (-1f, 1f, 1f);
			Vector3 hingePos = hinge.localPosition;
			hingePos.x = -hingePos.x;
			hinge.localPosition = hingePos;
		}
	}
}
