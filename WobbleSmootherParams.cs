namespace NInk;

public unsafe struct WobbleSmootherParams()
{
    /// <summary>
    /// The length of the window over which the moving average of speed and
    /// position are calculated.
    ///
    /// A good starting point is 2.5 divided by the expected number of inputs per
    /// unit time.
    /// </summary>
    public double timeout = 1;

    /// <summary>
    /// The range of speeds considered for wobble smoothing. At speed_floor, the
    /// maximum amount of smoothing is applied. At speed_ceiling, no smoothing is
    /// applied.
    ///
    /// Good starting points are 2% and 3% of the expected speed of the inputs.
    /// </summary>
    public float speed_floor = -1;

    /// <summary>
    /// The range of speeds considered for wobble smoothing. At speed_floor, the
    /// maximum amount of smoothing is applied. At speed_ceiling, no smoothing is
    /// applied.
    ///
    /// Good starting points are 2% and 3% of the expected speed of the inputs.
    /// </summary>
    public float speed_ceiling = -1;
}
