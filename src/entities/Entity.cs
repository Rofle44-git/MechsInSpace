using Godot;

public partial class Entity : CharacterBody2D {
	[Export(PropertyHint.Range, "0,32768,1")] public int MaxHP;
	[Export(PropertyHint.Range, "0,32768,1")] public int HP;
	[Export(PropertyHint.Range, "0,32768,1")] public int Speed;

	public void Heal(int amount) {
		if (HP>=MaxHP) {HP = MaxHP; _OnMaxHeal(); return;}
		HP+=amount; _OnHeal();
	}

	public void Harm(int amount) {
		HP-=amount; if (HP<=0) {_OnDeath(); return;}
		_OnHarm();
	}

	public virtual void _OnHeal() {}
	public virtual void _OnMaxHeal() {_OnHeal();}
	public virtual void _OnHarm() {}
	public virtual void _OnDeath() {_Die();}
	public virtual void _Die() {QueueFree();}
}
