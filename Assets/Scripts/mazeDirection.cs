using UnityEngine;

public enum mazeDirection
{
	North,
	South,
	East,
	West
}

public static class MazeDirections
{
	public const int Count = 4;

	public static mazeDirection RandomDirection()
	{
		return (mazeDirection)Random.Range (0, Count);
	}

	private static IntVector2[] vectors = {
		new IntVector2 (0, 1),
		new IntVector2 (0, -1),
		new IntVector2 (1, 0),
		new IntVector2 (-1, 0),
	};

	private static mazeDirection[] opposites = {
		mazeDirection.South,
		mazeDirection.North,
		mazeDirection.West,
		mazeDirection.East
	};

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0, 180, 0),
		Quaternion.Euler(0, 90, 0),
		Quaternion.Euler(0, 270, 0)
	};

	public static IntVector2 ToIntVector2(this mazeDirection dir)
	{
		return vectors [(int)dir];
	}

	public static mazeDirection GetOpposite(this mazeDirection dir)
	{
		return opposites [(int)dir];
	}

	public static Quaternion ToRotation(this mazeDirection dir)
	{
		return rotations [(int)dir];
	}
}