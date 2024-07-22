namespace NInk;

public unsafe struct StylusStateModelerParams()
{
    /// <summary>
    /// The maximum number of raw inputs to look at when finding the nearest states
    /// for interpolation.
    /// </summary>
    public int max_input_samples = 10;
}
