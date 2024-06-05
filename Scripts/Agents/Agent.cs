using Godot;

public partial class Agent : Sprite2D {
	[Export]
	public Stats Stats;

	[Export]
	public Ability[] Abilities = System.Array.Empty<Ability>();

	public float AtkTimer = 0;
}