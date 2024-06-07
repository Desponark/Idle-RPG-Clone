using System;
using Godot;

[GlobalClass]
public partial class PassiveEffect : Resource {
	[Export]
	public string Name;
	[Export]
	public int Level = 0;

	[Export]
	private ReactType reactType = 0;

	private enum ReactType {
		Thorns,
	}

	public void LevelUp(Agent agent) {
		if (agent.Stats.RP <= 0)
			return;
		agent.Stats.RP--;
		Level++;
	}

	internal float React(Agent self, Agent attackee, float damage) {
		if (Level <= 0)
			return 0;

		switch (reactType) {
			case ReactType.Thorns:
				var reflectedDmg = damage / 100 * Level;
				attackee.TakeDamage(reflectedDmg);
				Logger.Log($"{self.Name} used {Name} and reflected [color=red]{reflectedDmg} damage[/color] to {attackee.Name}");
				break;
		}

		return 0;
	}
}