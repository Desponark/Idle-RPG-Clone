using System;
using Godot;

[GlobalClass]
public partial class PassiveEffect : Resource {
	[Export]
	public string Name;
	[Export]
	public int Level = 0;
	[Export]
	public int IncreaseBy = 1;

	public void LevelUp(Agent agent) {
		if (agent.Stats.RP <= 0)
			return;
		agent.Stats.RP--;
		Level++;
	}
}