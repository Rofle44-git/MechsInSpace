using Godot;

public partial class HUD : Control {
	[Export] public Control CoinsContainer;
	[Export] AnimationPlayer AnimPlayer;
	[Export] Label CoinCountLabel;

	public void UpdateCoinCounter() {
		CoinCountLabel.Text = "Coins: " + Global.Coins.ToString();
		if (AnimPlayer.IsPlaying()) {AnimPlayer.Seek(0, true); return;}
		AnimPlayer.Play("OnCollect");
	}
}
