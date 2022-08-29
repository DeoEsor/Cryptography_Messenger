namespace NumberTheory.Interfaces;

public interface IPrimalChecker
{ 
    bool Check(BigInteger value, float minProbability);
    
    public static int GetDegree(float value)
    {
        int power = 1;
        while ((1 - Math.Pow(0.5, power)) < value ) power++;
        return power;
    }
}