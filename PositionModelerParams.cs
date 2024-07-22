namespace NInk;

public unsafe struct PositionModelerParams()
{
    /// <summary>The mass of the "weight" being pulled along the path, multiplied by the spring constant.</summary>
    public float spring_mass_constant = 11.0f / 32400;

    /// <summary>
    /// The ratio of the pen's velocity that is subtracted from the pen's
    /// acceleration per unit time, to simulate drag.
    /// </summary>
    public float drag_constant = 72.0f;
}
