using Godot;

[GlobalClass]
public partial class Stats : Node {
	// Attributes
	[Export]
	public int Strength = 1;
	[Export]
	public int Dexterity = 1;
	[Export]
	public int Intelligence = 1;
	[Export]
	public Attribute[] Attributes = System.Array.Empty<Attribute>();

	// Defensive
	[Export]
	public float MaxHP = 100;
	public float HP = 100;
	public float MaxSP = 100;
	public float SP = 100;
	public float MaxMP = 100;
	public float MP = 100;
	public float HPReg = 0.1f;
	public float SPReg = 0.1f;
	public float MPReg = 0.1f;

	// Offensive
	private float atk = 5;
	public float Atk {
		get {
			var retVal = atk;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModAtk(retVal);
			}
			return retVal;
		}
		set => atk = value;
	}
	public float MagAtk = 5;
	public float Acc = 100;
	public float AtkSpd = 3;

	// Other
	public float MoveSpd = 100;

	// Exp
	public int Lvl = 1;
	public int Exp = 0;
	public int ExpTotal = 0;
	public int ExpReq = 0;
	public int AP = 0;
	public int RP = 0;

}