using Godot;

public partial class CSGlobalBridge : Node {
	public override void _Ready() {
		Global.LoadContent();
	}
}
