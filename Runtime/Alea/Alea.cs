using System;

/// <summary>
/// Helper to manage randomness easier, without having to instantiate many Random
/// </summary>
public static class Alea
{
    private static Random rnd;

    static Alea()
    {
        rnd = new Random();
    }

    /// <summary>
    /// Returns a random int between min and max (max not included)
    /// min <= res < max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetInt(int min, int max)
    {
        return rnd.Next(min, max);
    }

    /// <summary>
    /// Returns a random int between min and max (max included)
    /// min <= res <= max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetIntInc(int min, int max)
    {
        return rnd.Next(min, max + 1);
    }

    /// <summary>
    /// Returns a random float between min and max (max not included)
    /// min <= res < max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float GetFloat(float min, float max)
    {
        return min + (float)rnd.NextDouble() * (max-min);
    }

    /// <summary>
    /// Returns a random double between min and and max (max not included)
    /// min <= res < max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static double GetDouble(double min, double max)
    {
        return min + rnd.NextDouble() * (max - min);
    }
}

