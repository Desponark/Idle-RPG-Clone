using Godot;

[GlobalClass]
public partial class Intelligence : Attribute {
	public override float ModMagAtk(float retVal) {
		return retVal + Value;
	}

	public override float ModMaxMp(float retVal) {
		return retVal + (Value * 10f);
	}

	public override float ModMPReg(float retVal) {
		return retVal * (1 + (Value / 10f));
	}
}