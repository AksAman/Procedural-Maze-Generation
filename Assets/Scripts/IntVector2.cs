using System;

[System.Serializable]
public struct IntVector2 {
	public int x, z;

	public IntVector2(int _x, int _z)
	{
		this.x = _x;
		this.z = _z;
	}

	public static IntVector2 operator + (IntVector2 a, IntVector2 b)
	{
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}
