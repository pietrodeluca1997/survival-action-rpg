using Godot;

[GlobalClass]
public sealed partial class LocomotionDebuggerComponent3D : Node
{
    [ExportGroup("Associations")]

    [Export]
    private LocomotionController3D LocomotionController { get; set; }

    [Export]
    private FloorDetectorComponent3D FloorDetectorComponent { get; set; }

    [Export] 
    private string LogPrefix { get; set; } = "[LocomotionDebugger3D]";

    [Export] 
    private bool LogFloorDetectionEvents { get; set; } = false;

    [Export] 
    private bool LogLocomotionStateData { get; set; } = false;

    public override void _Ready()
    {
        if (FloorDetectorComponent != null)
        {
            FloorDetectorComponent.OnFloorEntered += HandleFloorEntered;
            FloorDetectorComponent.OnFloorExited += HandleFloorExited;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (LogLocomotionStateData)
        {
            GD.Print($"{LogPrefix} {LocomotionController.CurrentLocomotionState}");
        }
    }

    private void HandleFloorEntered()
    {
        if (LogFloorDetectionEvents)
        {
            GD.Print($"{LogPrefix} Entered Floor.");
        }
    }

    private void HandleFloorExited()
    {
        if (LogFloorDetectionEvents)
        {
            GD.Print($"{LogPrefix} Exited Floor.");
        }
    }
}
