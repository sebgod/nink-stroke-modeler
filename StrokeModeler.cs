using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace NInk;

public partial class StrokeModeler(
    WobbleSmootherParams wobbleParams = default,
    PositionModelerParams positionModelerParams = default,
    SamplingParams samplingParams = default,
    StylusStateModelerParams stylusStateModelerParams = default 
) : IDisposable
{
    private readonly IntPtr _modeler = NewStrokeModeler(wobbleParams, positionModelerParams, samplingParams, stylusStateModelerParams);

    private bool disposedValue;

    public unsafe SafeHandle Update(EventType eventType, in Vec2 position, DateTime time, out Result* results, out int resultsCount)
    {
        Update(_modeler, eventType, position, time.ToOADate(), out var resultListHandle, out results, out resultsCount);

        return resultListHandle;
    }

    [LibraryImport("nink", EntryPoint = "nink_init_modeler")]
    internal static partial nint NewStrokeModeler(
        WobbleSmootherParams wobbleParams,
        PositionModelerParams positionModelerParams,
        SamplingParams samplingParams,
        StylusStateModelerParams stylusStateModelerParams
    );

    [LibraryImport("nink", EntryPoint = "nink_init_modeler")]
    internal static partial void FreeStrokeModeler(nint modeler);

    [LibraryImport("nink", EntryPoint = "nink_update")]
    internal static unsafe partial void Update(
        nint modeler,
        EventType eventType,
        Vec2 position,
        double time,
        out ItemsSafeHandle resultListHandle,
        out Result* results,
        out int resultsCount
    );

    [LibraryImport("nink", EntryPoint = "nink_release_results_handle")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ReleaseResultListHandle(nint hItems);

    public unsafe struct Result()
    {
        /// <summary>
        /// The position of the stroke tip.
        /// </summary>
        public Vec2 position;

        /// <summary>
        /// The velocity of the stroke tip.
        /// </summary>
        public Vec2 velocity;

        /// <summary>
        /// The acceleration of the stroke tip.
        /// </summary>
        public Vec2 acceleration;

        /// <summary>
        /// The time at which this input occurs.
        /// </summary>
        public double time;

        /// <summary>
        /// Pressure of the stylus. See the corresponding fields on the Input struct for more info.
        /// </summary>
        public float pressure = -1;

        /// <summary>
        /// Tilt of the stylus. See the corresponding fields on the Input struct for more info.
        /// </summary>
        public float tilt = -1;

        /// <summary>
        /// Orientation of the stylus. See the corresponding fields on the Input struct for more info.
        /// </summary>
        public float orientation = -1;
    }
    
    internal class ItemsSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public ItemsSafeHandle() : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return ReleaseResultListHandle(handle);
        }

    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            FreeStrokeModeler(_modeler);
            disposedValue = true;
        }
    }

    ~StrokeModeler()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}