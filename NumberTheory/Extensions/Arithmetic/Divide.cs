namespace NumberTheory.Extensions.Arithmetic
{
	public static partial class ArithmeticExtensions
	{
		/// <summary>
		/// Binary implementation of divide (unsigned)
		/// </summary>
		/// <param name="dividend"></param>
		/// <param name="divisor"></param>
		/// <returns>
		/// <para>First item - quotient</para> 
		/// <para>Second item - remainder</para>
		/// </returns>
		public static (uint,uint) unsigned_divide(uint dividend, uint divisor)
		{
			uint quotient, remainder; 
			uint t, num_bits;
			uint q = 0, bit = 0, d = 0;

			remainder = 0;
			quotient = 0;

			if (divisor == 0)
				return (UInt32.MaxValue, UInt32.MaxValue);

			if (divisor > dividend) 
			{
				remainder = dividend;
				return (quotient, remainder);
			}

			if (divisor == dividend) 
			{
				quotient = 1;
				return (quotient, remainder);
			}

			num_bits = 32;

			while (remainder < divisor) 
			{
				bit = (dividend & 0x80000000) >> 31;
				remainder = (remainder << 1) | bit;
				d = dividend;
				dividend = dividend << 1;
				num_bits--;
			}


			/* The loop, above, always goes one iteration too far.
				To avoid inserting an "if" statement inside the loop
				the last iteration is simply reversed. */

			dividend = d;
			remainder = remainder >> 1;
			num_bits++;

			for (var i = 0; i < num_bits; i++) 
			{
				bit = (dividend & 0x80000000) >> 31;
				remainder = (remainder << 1) | bit;
				t = remainder - divisor;
				q = (t >> 31) == 0 ? (uint)1 : 0;
				dividend = dividend << 1;
				quotient = (quotient << 1) | q;
				if ((q & 0b1) == 1) 
					remainder = t;
			}
			return (quotient , remainder);
		}
	}
}
