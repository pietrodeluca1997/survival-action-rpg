using Godot;

[GlobalClass]
public sealed partial class GravityComponent3D : Node
{
    [ExportGroup("Gravity Properties")]

    [Export(PropertyHint.Range, "0.0f,100.0f,1.0f")]
    private float GravityForce { get; set; } = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    [Export]
    private bool Enabled { get; set; } = true;

    public void Update(ref LocomotionStateData locomotionState, float deltaTime)
    {
        if (!Enabled)
        {
            locomotionState.Velocity.Y = 0f;
            return;
        }

        if (!locomotionState.IsOnFloor)
        {
            locomotionState.Velocity.Y -= GravityForce * deltaTime;
        }
        else
        {
            locomotionState.Velocity.Y = 0f;
        }
    }
}
