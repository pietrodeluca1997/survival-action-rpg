using Godot;

[GlobalClass]
public sealed partial class LocomotionController3D : Node
{
    private LocomotionStateData _locomotionState = new();

    [ExportGroup("Associations")]

    [Export(PropertyHint.NodeType, "CharacterBody3D")]
    private CharacterBody3D TargetBody { get; set; }

    [Export]
    private FloorDetectorComponent3D FloorDetectorComponent { get; set; }

    [Export]
    private GravityComponent3D GravityComponent { get; set; }

    [Export]
    private HumanoidLocomotionComponent3D HumanoidLocomotionComponent { get; set; }

    [ExportGroup("Locomotion Properties")]

    [Export]
    private bool ShouldUseDirectionReference { get; set; }

    [Export]
    private Node3D MovementDirectionReference { get; set; }


    public LocomotionStateData CurrentLocomotionState => _locomotionState;

    public override void _PhysicsProcess(double deltaTime)
    {
        float deltaTimeFloat = (float) deltaTime;

        if (FloorDetectorComponent != null)
        {
            FloorDetectorComponent.Update(ref _locomotionState);
        }

        if (GravityComponent != null)
        {
            GravityComponent.Update(ref _locomotionState, deltaTimeFloat);
        }

        if (HumanoidLocomotionComponent != null)
        {
            HumanoidLocomotionComponent.Update(ref _locomotionState, deltaTimeFloat);
        }

        TargetBody.Velocity = _locomotionState.Velocity;
        TargetBody.MoveAndSlide();
    }

    public void SetDirection(Vector2 direction)
    {
        Vector3 direction3D = new Vector3(direction.X, 0.0f, direction.Y).Normalized();

        if (ShouldUseDirectionReference && MovementDirectionReference != null)
        {
            direction3D = MovementDirectionReference.GlobalTransform.Basis * direction3D;
        }

        _locomotionState.Direction = direction3D;
    }
}
