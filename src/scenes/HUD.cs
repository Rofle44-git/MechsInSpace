using Godot;

public partial class HUD : Control {
	[Export] public Control CoinsContainer;
	[Export] public Control Shockwaves;
	[Export] AnimationPlayer AnimPlayer;
	[Export] Label CoinCountLabel;
	[Export] AudioStreamPlayer CoinSFX;

	public void UpdateCoinCounter() {
		CoinSFX.Play();
		CoinCountLabel.Text = "Coins: " + Global.Coins.ToString();
		if (AnimPlayer.IsPlaying()) {AnimPlayer.Seek(0, true); return;}
		AnimPlayer.Play("OnCollect");
	}
}
