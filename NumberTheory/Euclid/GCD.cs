
// ReSharper disable InconsistentNaming
namespace NumberTheory.Euclid;

public static class GCD
{
	public static int Solve (int val1, int val2)
	{
		while (val1 !=0 && val2 != 0)
			if (val1 > val2)
				val1 -= val2;
			else
				val2 -= val1;
        
		return Math.Max(val1, val2);
	}
}