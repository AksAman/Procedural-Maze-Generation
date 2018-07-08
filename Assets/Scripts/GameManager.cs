using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Maze mazeprefab;
	private Maze mazeInstance;

	void Start()
	{
		BeginGame ();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartGame ();
		}
	}

	void BeginGame ()
	{
		//Create a maze
//		Debug.Log ("New Game Started......!");
		mazeInstance = Instantiate (mazeprefab) as Maze;

		// Generate maze
		mazeInstance.Generate ();
	}

	void RestartGame ()
	{
//		Debug.Log ("Restarting......!");
//		StopAllCoroutines ();
//		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);
		BeginGame ();
	}
}
