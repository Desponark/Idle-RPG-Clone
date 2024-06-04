using Godot;

[GlobalClass]
public partial class Strength : Attribute {
	public override float ModAtk(float retVal) {
		return retVal + Value;
	}
}