using UnityEngine;


public class CellI2
{
	public readonly int i;
	public readonly int j;

	public CellI2(int i, int j)
	{
		this.i = i;
		this.j = j;
	}

	public CellI2 Add(int i, int j)
	{
		return new CellI2(this.i + i, this.j + j);
	}

	public Vector3 ToVector3(float f)
	{
		return new Vector3(i * f, 0, j * f);
	}

	public override bool Equals(object obj)
	{
		var other = obj as CellI2;

		return null != other && ToString() == ("" + other);
	}


	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}

	public override string ToString()
	{
		return GetType().Name + "(" + (i > 0 ? "+" + i : "" + i) + "," + (j > 0 ? "+" + j : "" + j) + ")";
	}
}