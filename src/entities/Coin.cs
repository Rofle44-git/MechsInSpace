using Godot;

public partial class Coin : RigidBody2D {
	[Export] PackedScene DisappearEffect;

	public void GetCollected() {
		// HOW DOES IT WORK
		//Global.EmitSignal(SignalName.CoinPickedUp);
		Node2D effect = DisappearEffect.Instantiate<Node2D>();
		effect.GlobalPosition = GlobalPosition;
		AddSibling(effect);
		QueueFree();
	}

	private void _on_collect_area_body_entered(Node2D body) {
		if (body == Global.Player) {
			// GO GO GO START MOVING TOD A UHHHH PLAYER FROM GLOBAL:PLAYER NOW
		}
	}
}
