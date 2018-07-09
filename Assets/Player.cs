using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	RaycastHit hit;
	void Update()
	{
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,50f))
		{
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 1f, Color.white);

			if(hit.transform.tag == "Door")
			{
//				Debug.Log (hit.transform.name);
				if (Input.GetMouseButtonDown (2)) {
					MazeDoor door;
					if(hit.transform.parent.name != "Hinge")
					{
						door = hit.transform.parent.GetComponent<MazeDoor> ();
					}
					else
					{
						door = hit.transform.parent.parent.GetComponent<MazeDoor> ();
					}
						
					if (!door.isOpen)
						door.OpenDoor ();
					else if (door.isOpen)
						door.CloseDoor ();
				}

			}
		}
	}
}
