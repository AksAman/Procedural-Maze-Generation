using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

	public Maze mazeprefab;
	private Maze mazeInstance;

	public GameObject playerPrefab;
	private GameObject playerInstance;

	public Camera minimapCam;
	int zoomAmountScale = 3;
	int defaultOrthoSize = 5;

//	public NavMeshSu
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


		float zoomAmount = Input.GetAxis ("Mouse ScrollWheel");
		minimapCam.orthographicSize -= zoomAmount * zoomAmountScale;
	}


	void BeginGame ()
	{
		//Create a maze
//		Debug.Log ("New Game Started......!");
		mazeInstance = Instantiate (mazeprefab) as Maze;
		// Generate maze
		mazeInstance.Generate ();

		playerInstance = Instantiate (playerPrefab) as GameObject;
		playerInstance.transform.position = mazeInstance.GetCell (mazeInstance.RandomCoordinates).transform.position;
		minimapCam.GetComponent<MiniMap> ().player = playerInstance.transform;
		minimapCam.orthographicSize = defaultOrthoSize;
	}

	void RestartGame ()
	{
		Destroy (playerInstance.gameObject);
		Destroy (mazeInstance.gameObject);
		BeginGame ();
	}
}
