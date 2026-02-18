using Godot;

public struct LocomotionStateData
{
    public Vector3 Direction;
    public Vector3 Velocity;    
    public bool IsOnFloor;

    public override readonly string ToString()
    {
        return $"Direction: {Direction} - Velocity: {Velocity} - IsOnFloor {IsOnFloor}";
    }
}