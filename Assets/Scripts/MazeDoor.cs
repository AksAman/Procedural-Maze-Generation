using UnityEngine;

public class MazeDoor : MazePassage {

	public Transform hinge;
	public bool isOpen;

	private MazeDoor OtherSideDoor{
		get
		{
			return otherCell.GetEdge (direction.GetOpposite()) as MazeDoor;
		}
	}

	public override void Initialise(MazeCell _cell, MazeCell _otherCell, mazeDirection _direction)
	{
		base.Initialise (_cell, _otherCell, _direction);
		this.isOpen = false;

		if(OtherSideDoor != null)
		{
			hinge.localScale = new Vector3 (-1f, 1f, 1f);
			Vector3 hingePos = hinge.localPosition;
			hingePos.x = -hingePos.x;
			hinge.localPosition = hingePos;
		}

		for(int i=0; i<transform.childCount; i++)
		{
			Transform doorTransform = transform.GetChild (i);
			if(doorTransform != hinge)
			{
				doorTransform.GetComponent<Renderer> ().material = _cell.room.setting.wallMaterial;
			}
		}
	}

	public void OpenDoor()
	{
		hinge.localRotation = OtherSideDoor.hinge.localRotation = Quaternion.Euler (0f, 90f, 0f);
		isOpen = true;
	}

	public void CloseDoor()
	{
		hinge.localRotation = OtherSideDoor.hinge.localRotation = Quaternion.Euler (0f, 0f, 0f);
		isOpen = false;
	}
}
