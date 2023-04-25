using Godot;
using System;

public partial class AnimatedButton : Button {
    [Export] Vector2 ExpandAmount = new Vector2(64, 0);
    Vector2 OriginalSize;

    public override void _Ready() {
        MouseEntered += () => OnMouseEntered();
        MouseExited += () => OnMouseExited();
        OriginalSize = Size;
    }

    private void OnMouseEntered() {
        GetDefaultTween().TweenProperty(this, "size", OriginalSize+ExpandAmount, 0.4);
    }

    private void OnMouseExited() {
        GetDefaultTween().TweenProperty(this, "size", OriginalSize, 0.4);
    }

    private Tween GetDefaultTween() {
        Tween expandTween = CreateTween();
        expandTween.SetEase(Tween.EaseType.Out);
        expandTween.SetTrans(Tween.TransitionType.Back);
        return expandTween;
    }
}
