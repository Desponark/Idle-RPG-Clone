using Godot;

[GlobalClass]
public partial class Attribute : Resource {
	[Export]
	public int Value = 1;

	public virtual float ModAtk(float retVal) {
		return retVal;
	}

	public virtual float ModMagAtk(float retVal) {
		return retVal;
	}
}