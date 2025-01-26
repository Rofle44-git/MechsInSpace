using Godot;

public partial class Entity : CharacterBody2D
{
	[Signal] public delegate void DeathEventHandler();
	[Export(PropertyHint.Range, "0,32768,1")] public float MaxHP = 100f;
	[Export(PropertyHint.Range, "0,32768,1")] public float HP = 100f;
	[Export(PropertyHint.Range, "0,32768,1")] public float Speed = 350f;

	public void Heal(float amount)
	{
		if (HP >= MaxHP) { HP = MaxHP; OnMaxHeal(); return; }
		HP += amount; OnHeal();
	}

	public void Harm(float amount)
	{
		HP -= amount; if (HP <= 0f) { OnDeath(); return; }
		OnHarm();
	}

	public virtual void OnHeal()
	{
	}
	public virtual void OnMaxHeal()
	{
		OnHeal();
	}
	public virtual void OnHarm()
	{
	}
	public virtual void OnDeath()
	{
		EmitSignal(SignalName.Death);
		Die();
	}
	public void Die()
	{
		QueueFree();
	}
}
