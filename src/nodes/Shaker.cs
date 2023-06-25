using Godot;

public partial class Shaker : Node {
	[Export] Node TargetNode;
	[Export] StringName TargetProperty;
	[Export] StringName PropertyType;
	[Export] float MinValue = 0.0f;
	[Export] float MaxValue = 0.0f;
	[Export] bool Constant = false;
	[Export] bool AutoStart = false;
	[Export(PropertyHint.Range, "0,3600,0.1")] float Duration = 0.8f;
	[Export] Curve FallOff;
	Timer DurationTimer = new Timer();
	float InterpolatedTime;

	public override void _Ready() {
		SetProcess(false);
		TargetNode = (TargetNode == null) ? GetParent<Node>() : TargetNode;
		AddChild(DurationTimer);
		DurationTimer.WaitTime = Duration;
		DurationTimer.Timeout += () => Stop();
		if (AutoStart) Start();
	}

	public override void _Process(double delta) {
		InterpolatedTime = (Constant) ? 1.0f : 1.0f - FallOff.Sample((float)DurationTimer.TimeLeft / Duration);
		switch (PropertyType) {
			case "int":
				TargetNode.Set(TargetProperty, (int)GD.RandRange(MinValue, MaxValue)*InterpolatedTime);
				break;
			
			case "float":
				TargetNode.Set(TargetProperty, GD.RandRange(MinValue, MaxValue)*InterpolatedTime);
				break;
			
			case "vector2i":
				TargetNode.Set(TargetProperty, new Vector2I(
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
			
			case "vector2":
				TargetNode.Set(TargetProperty, new Vector2(
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
			
			case "vector3i":
				TargetNode.Set(TargetProperty, new Vector3I(
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
			
			case "vector3":
				TargetNode.Set(TargetProperty, new Vector3(
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
			
			case "vector4i":
				TargetNode.Set(TargetProperty, new Vector4I(
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(int)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
			
			case "vector4":
				TargetNode.Set(TargetProperty, new Vector4(
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime),
					(float)(GD.RandRange(MinValue, MaxValue)*InterpolatedTime)
				));
				break;
		}
	}

	public void Start() {
		DurationTimer.Start();
		SetProcess(true);
	}

	public void Stop() {
		DurationTimer.Stop();
		SetProcess(false);
	}
}