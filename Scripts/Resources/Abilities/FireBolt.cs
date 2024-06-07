using Godot;

[GlobalClass]
public partial class FireBolt : Ability {
	public override void Execute(Agent self, Agent target) {
		if (target == null)
			return;

		if (Level <= 0)
			return;

		if (self.Stats.MP < CastCost)
			return;

		self.Stats.MP -= CastCost;

		var damage = BaseDamage * Level * (1 + (self.Stats.MagAtk / 100));
		target.TakeDamage(damage);

		Logger.Log($"{self.Name} used [color=orange]{Name}[/color] on {target.Name} doing [color=red]{damage:0} damage[/color]");
	}
}