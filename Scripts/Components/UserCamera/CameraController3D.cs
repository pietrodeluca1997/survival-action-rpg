using Godot;

[GlobalClass]
public sealed partial class CameraController3D : Node
{
    private Vector2 _springArmRotationDirection = Vector2.Zero;

    [ExportGroup("Associations")]
    
    [Export(PropertyHint.NodeType, "SpringArm3D")]
    private SpringArm3D SpringArm { get; set; }

    [Export(PropertyHint.NodeType, "Camera3D")]
    private Camera3D Camera { get; set; }

    [Export]
    private Node3D HorizontalCameraPivot { get; set; }

    [Export]
    private Node3D VerticalCameraPivot { get; set; }

    [ExportGroup("Movement Properties")]

    [Export]
    private float MinCameraRotationBoundary { get; set; } = -60.0f;

    [Export]
    private float MaxCameraRotationBoundary { get; set; } = 10.0f;

    [Export]
    private float SpringArmDecay { get; set; } = 10.0f;

    public override void _PhysicsProcess(double delta)
    {
        SpringArm.GlobalTransform = SpringArm.GlobalTransform.InterpolateWith(
            VerticalCameraPivot.GlobalTransform,
            (float) (1.0f - Mathf.Exp(-SpringArmDecay * delta))
        );

        HorizontalCameraPivot.RotateY(_springArmRotationDirection.X);
        VerticalCameraPivot.RotateX(_springArmRotationDirection.Y);

        VerticalCameraPivot.Rotation = new Vector3(
            Mathf.Clamp(VerticalCameraPivot.Rotation.X, Mathf.DegToRad(MinCameraRotationBoundary), Mathf.DegToRad(MaxCameraRotationBoundary)),
            VerticalCameraPivot.Rotation.Y,
            VerticalCameraPivot.Rotation.Z
        );

        _springArmRotationDirection = Vector2.Zero;
    }

    public void SetSpringArmRotationDirection(Vector2 rotationDirection)
    {
        _springArmRotationDirection += rotationDirection;
    }
}
