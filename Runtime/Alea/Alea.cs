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
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetIntInc(int min, int max)
    {
        return rnd.Next(min, max + 1);
    }
}

