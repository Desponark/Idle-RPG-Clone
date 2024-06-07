using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PassiveAttribute : Resource {
	[Export]
	public string Name;
	[Export]
	public int Level = 0;
	[Export]
	public int IncreaseBy = 1;
	[Export]
	public Array<Attribute> attributes = new();

	public void LevelUp(Agent agent) {
		if (agent.Stats.RP <= 0)
			return;
		agent.Stats.RP--;
		Level++;
	}

	public void SetAttributeValues(Array<Attribute> attrs) {
		foreach (var attr in attrs) {
			var res = attributes.FirstOrDefault(x => x.GetType() == attr.GetType());
			if (res != null) {
				attr.Value = attr.BaseValue + (IncreaseBy * Level);
			}
			else {
				attr.Value = attr.BaseValue;
			}
		}
	}
}