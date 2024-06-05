using Godot;

[GlobalClass]
public abstract partial class Ability : Resource {
	[Export]
	public string Name;

	public int Level = 0;
	[Export]
	public int CastCost = 20;
	[Export]
	public int BaseDamage = 5;
	[Export]
	private int unlockCost = 1;
	public int UpgradeCost { get => unlockCost + Level; }
	[Export]
	public PackedScene scene = ResourceLoader.Load<PackedScene>("res://Scenes/Abilities/AbilityButton.tscn");

	public bool IsUnlocked() {
		return Level > 0;
	}

	public virtual void Upgrade(Agent self) {
		if (self.Stats.RP < UpgradeCost)
			return;

		self.Stats.RP -= UpgradeCost;
		Level++;
	}

	public abstract void Execute(Agent self, Agent target);
}