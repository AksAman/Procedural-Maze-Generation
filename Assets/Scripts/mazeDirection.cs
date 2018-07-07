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
		new IntVector2 (-1, 0),
		new IntVector2 (1, 0),
	};

	public static IntVector2 ToIntVector2(this MazeDirection dir)
	{
		return vectors [(int)dir];
	}
}