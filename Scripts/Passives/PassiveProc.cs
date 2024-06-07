using System;
using Godot;

[GlobalClass]
public partial class PassiveProc : Resource {
	[Export]
	public string Name;
	[Export]
	public int Level = 0;

	[Export]
	private ProcType procType = 0;

	private enum ProcType {
		LifeSteal,
	}

	public void LevelUp(Agent agent) {
		if (agent.Stats.RP <= 0)
			return;
		agent.Stats.RP--;
		Level++;
	}

	internal void Proc(Agent self, Agent target) {
		if (Level <= 0)
			return;

		switch (procType) {
			case ProcType.LifeSteal:
				var amount = (int)((self.Stats.Atk / 50) * Level);
				self.HealHp(amount);
				Logger.Log($"{self.Name} used {Name} and healed for [color=green]{amount} health[/color]");
				break;
		}

	}
}