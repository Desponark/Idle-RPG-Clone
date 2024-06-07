using Godot;

[GlobalClass]
public abstract partial class Attribute : Resource {
	[Export]
	public int BaseValue = 1;

	public int Value = 1;

	[Export]
	public PackedScene Scene = ResourceLoader.Load<PackedScene>("res://Scenes/HUD/Attribute.tscn");
	// public PackedScene Scene;

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