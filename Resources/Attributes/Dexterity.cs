using Godot;

[GlobalClass]
public partial class Dexterity : Attribute {
	public override float ModAtkSpd(float retVal) {
		return retVal - (Value / 100f);
	}
}