using UnityEngine;

public enum MazeDirection
{
	North,
	South,
	East,
	West
}

public static class MazeDirections
{
	public const int count = 4;

	public static MazeDirection RandomDirection()
	{
		return (MazeDirection)Random.Range (0, count);
	}

	private static IntVector2[] vectors = {
		new IntVector2 (0, -1),
		new IntVector2 (0, -1),
		new IntVector2 (1, 0),
		new IntVector2 (-1, 0),
	};

	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.North,
		MazeDirection.West,
		MazeDirection.East
	};

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0, 180, 0),
		Quaternion.Euler(0, 90, 0),
		Quaternion.Euler(0, -90, 0)
	};

	public static IntVector2 ToIntVector2(this MazeDirection dir)
	{
		return vectors [(int)dir];
	}

	public static MazeDirection GetOpposite(this MazeDirection dir)
	{
		return opposites [(int)dir];
	}

	public static Quaternion ToRotation(this MazeDirection dir)
	{
		return rotations [(int)dir];
	}
}