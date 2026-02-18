using Godot;

[GlobalClass]
public sealed partial class FloorDetectorComponent3D : Node
{
    private bool _wasOnFloor;

    [Signal] public delegate void OnFloorEnteredEventHandler();
    [Signal] public delegate void OnFloorExitedEventHandler();

    [ExportGroup("Associations")]

    [Export(PropertyHint.NodeType, "CharacterBody3D")]
    private CharacterBody3D TargetBody { get; set; }

    public override void _Ready()
    {
        _wasOnFloor = TargetBody.IsOnFloor();
    }

    public void Update(ref LocomotionStateData locomotionState)
    {
        bool isOnFloor = TargetBody.IsOnFloor();

        if (isOnFloor && !_wasOnFloor)
        {
            EmitSignal(SignalName.OnFloorEntered);
        }
        else if (!isOnFloor && _wasOnFloor)
        {
            EmitSignal(SignalName.OnFloorExited);
        }

        locomotionState.IsOnFloor = isOnFloor;
        _wasOnFloor = isOnFloor;
    }
}
