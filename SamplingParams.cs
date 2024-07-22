namespace NInk;

public unsafe struct  SamplingParams()
{
    /// <summary>
    /// The minimum number of modeled inputs to output per unit time. If inputs are
    /// received at a lower rate, they will be upsampled to produce output of at
    /// least min_output_rate. If inputs are received at a higher rate, the
    /// output rate will match the input rate.
    /// </summary>
    public double min_output_rate = -1;

    /// <summary>This determines stop condition for end-of-stroke modeling; if the position
    /// is within this distance of the final raw input, or if the last update
    /// iteration moved less than this distance, it stop iterating.
    ///
    /// This should be a small distance; a good starting point is 2-3 orders of
    /// magnitude smaller than the expected distance between input points.
    /// </summary>
    public float end_of_stroke_stopping_distance = -1;

    /// <summary>
    /// The maximum number of iterations to perform at the end of the stroke, if it
    // does not stop due to the constraints of end_of_stroke_stopping_distance.
    /// </summary>
    public int end_of_stroke_max_iterations = 20;

    /// <summary>
    /// Maximum number of outputs to generate per call to Update or Predict.
    /// This limit avoids crashes if input events are received with too long of
    /// a time between, possibly because a client was suspended and resumed.
    /// </summary>
    public int max_outputs_per_call = 100000;

    /// <summary>
    /// Max absolute value of estimated angle to traverse in a single upsampled
    /// input step in radians (0, PI). The traversed angle is estimated by
    /// considering the change in the angle of the tip state that would happen due
    /// to the input without any upsampling. If set to -1 (the default), input is
    /// not upsampled for this reason.
    /// </summary>
    public double max_estimated_angle_to_traverse_per_input = -1;
}
