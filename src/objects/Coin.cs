using Godot;

public partial class Coin : Sprite2D {
    public async override void _Ready() {
        Vector2 randOffset = new Vector2(
            GD.RandRange(-160, 160),
            GD.RandRange(-160, 160)
        );

        Tween tween = CreateTween();
        tween.SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.Out)
            .TweenProperty(this, "global_position", GlobalPosition+randOffset, GD.RandRange(0.07, 0.6));
        
        await ToSignal(tween, "finished");

        tween = CreateTween();
        tween.SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.In)
            .TweenProperty(this, "global_position", Global.CoinGoalPos, GD.RandRange(0.07, 0.6));
        
        tween.Finished += () => _OnArrived();
    }

    private void _OnArrived() {
        Global._OnCoinCollected();
        QueueFree();
    }
}
