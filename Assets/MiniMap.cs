using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

	public Transform player;

	void LateUpdate()
	{
		if(player != null)
		{
			transform.position = new Vector3 (player.position.x, transform.position.y, player.position.z);
			transform.rotation = Quaternion.Euler (90f, player.rotation.eulerAngles.y, 0f);
		}
	}
}
