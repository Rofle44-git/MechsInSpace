using Godot;

public partial class ShakeableCamera2D : Camera2D {
   const float Duration = 0.8f;
	const int ShakeRange = 16;
	public Vector2 Pan;
	Godot.Collections.Array<CameraShaker> CachedShakers = new Godot.Collections.Array<CameraShaker>();

	public override void _Process(double delta) {
		float strengthSum = 0f;
		foreach (CameraShaker shaker in CachedShakers) {
			strengthSum += shaker.StrengthMultiplier;
		}
		float totalStrength = strengthSum*ShakeRange;

		Offset = new Vector2(
			Pan.X+(float)GD.RandRange(-totalStrength, totalStrength),
			Pan.Y+(float)GD.RandRange(-totalStrength, totalStrength)
		);
	}

	// <summary> Ranges from 0 to 2 </summary>
	public void Shake(int level = 1) {
		CameraShaker shaker = new CameraShaker(new float[] {0.4f, 1.0f, 2.5f}[level]);
		shaker.TreeEntered += () => CachedShakers.Add(shaker);
		shaker.TreeExiting += () => CachedShakers.Remove(shaker);
		AddChild(shaker);
	}
}
