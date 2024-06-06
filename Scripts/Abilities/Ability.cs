using Godot;

[GlobalClass]
public abstract partial class Ability : Resource {
	[Export]
	public string Name;
	[Export]
	public PackedScene Scene = ResourceLoader.Load<PackedScene>("res://Scenes/HUD/AbilityButton.tscn");

	public int Level = 0;
	[Export]
	public int CastCost = 20;
	[Export]
	public int BaseDamage = 5;
	[Export]
	private int unlockCost = 1;
	public int UpgradeCost { get => unlockCost + Level; }

	public bool IsUnlocked() {
		return Level > 0;
	}

	public virtual void Upgrade(Agent self) {
		if (self.Stats.RP < UpgradeCost)
			return;

		self.Stats.RP -= UpgradeCost;
		Level++;

		Logger.Log($"{self.Name} upgraded [color=orange]{Name}[/color] to [color=blue]{Level} level[/color]");
	}

	public abstract void Execute(Agent self, Agent target);
}