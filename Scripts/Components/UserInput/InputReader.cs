using Godot;

[GlobalClass]
public sealed partial class InputReader : Node
{
    [ExportGroup("Associations")]

    [Export]
    private LocomotionController3D LocomotionController { get; set; }

    [Export]
    private CameraController3D CameraController { get; set; }


    [ExportGroup("Input Properties")]

    [Export]
    private float MouseSensitivity { get; set; } = 0.00075f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

        LocomotionController.SetDirection(inputDirection);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
        }

        if (Input.MouseMode == Input.MouseModeEnum.Captured && @event is InputEventMouseMotion mouseMotion)
        {
            CameraController.SetSpringArmRotationDirection(-mouseMotion.Relative * MouseSensitivity);
        }
    }
}
