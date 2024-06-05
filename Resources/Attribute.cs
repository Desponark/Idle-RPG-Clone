using Godot;

[GlobalClass]
public partial class Attribute : Resource {
	[Export]
	public int Value = 1;

	public virtual float ModMaxHp(float retVal) {
		return retVal;
	}

	public virtual float ModMaxSp(float retVal) {
		return retVal;
	}

	public virtual float ModMaxMp(float retVal) {
		return retVal;
	}

	public virtual float ModHPReg(float retVal) {
		return retVal;
	}

	public virtual float ModSPReg(float retVal) {
		return retVal;
	}

	public virtual float ModMPReg(float retVal) {
		return retVal;
	}

	public virtual float ModAtk(float retVal) {
		return retVal;
	}

	public virtual float ModMagAtk(float retVal) {
		return retVal;
	}

	public virtual float ModAcc(float retVal) {
		return retVal;
	}

	public virtual float ModAtkSpd(float retVal) {
		return retVal;
	}

	public virtual float ModMoveSpd(float retVal) {
		return retVal;
	}
}