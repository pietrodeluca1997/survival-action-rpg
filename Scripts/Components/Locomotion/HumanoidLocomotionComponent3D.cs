using Godot;

[GlobalClass]
public sealed partial class HumanoidLocomotionComponent3D : Node
{
    [ExportGroup("Locomotion Properties")]

    [Export(PropertyHint.Range, "0.0f,100.0f,1.0f")] 
    private float MovementSpeed { get; set; } = 5.0f;

    [Export(PropertyHint.Range, "0.0f,100.0f,1.0f")]
    private float Acceleration { get; set; } = 20.0f;

    [Export(PropertyHint.Range, "0.0f,100.0f,1.0f")]
    private float Deceleration { get; set; } = 25.0f;

    public void Update(ref LocomotionStateData locomotionState, float deltaTimeFloat)
    {
        if (locomotionState.Direction.LengthSquared() > 0.0f)
        {
            Vector3 intent = locomotionState.Direction * MovementSpeed;

            locomotionState.Velocity.X = Mathf.MoveToward(locomotionState.Velocity.X, intent.X, Acceleration * deltaTimeFloat);
            locomotionState.Velocity.Z = Mathf.MoveToward(locomotionState.Velocity.Z, intent.Z, Acceleration * deltaTimeFloat);
        }
        else
        {
            locomotionState.Velocity.X = Mathf.MoveToward(locomotionState.Velocity.X, 0.0f, Deceleration * deltaTimeFloat);
            locomotionState.Velocity.Z = Mathf.MoveToward(locomotionState.Velocity.Z, 0.0f, Deceleration * deltaTimeFloat);
        }

    }
}