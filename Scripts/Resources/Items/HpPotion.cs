using System;
using Godot;

[GlobalClass]
public partial class HpPotion : Item {
	[Export]
	public int HealAmount = 10;

	public override event Action<Item> Used;

	public override void Execute(Agent agent) {
		agent.HealHp(HealAmount);

		Used.Invoke(this);

		Logger.Log($"{agent.Name} used [color=silver]{Name}[/color] healing [color=green]{HealAmount} health[/color]");
	}
}