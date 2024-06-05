using Godot;

[GlobalClass]
public partial class Stats : Node {
	// Attributes
	[Export]
	public Attribute[] Attributes = System.Array.Empty<Attribute>();

	// Defensive
	[Export]
	private float maxHP = 100;
	public float MaxHP {
		get {
			var retVal = maxHP;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMaxHp(retVal);
			}
			return retVal;
		}
		set => maxHP = value;
	}

	[Export]
	private float hp = 100;
	public float HP { get => hp; set => hp = value; }

	[Export]
	private float maxSP = 100;
	public float MaxSP {
		get {
			var retVal = maxSP;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMaxSp(retVal);
			}
			return retVal;
		}
		set => maxSP = value;
	}

	[Export]
	private float sp = 100;
	public float SP { get => sp; set => sp = value; }

	[Export]
	private float maxMP = 100;
	public float MaxMP {
		get {
			var retVal = maxMP;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMaxMp(retVal);
			}
			return retVal;
		}
		set => maxMP = value;
	}

	[Export]
	private float mp = 100;
	public float MP { get => mp; set => mp = value; }

	[Export]
	private float hpReg = 0.1f;
	public float HPReg {
		get {
			var retVal = hpReg;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModHPReg(retVal);
			}
			return retVal;
		}
		set => hpReg = value;
	}

	[Export]
	private float spReg = 0.1f;
	public float SPReg {
		get {
			var retVal = spReg;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModSPReg(retVal);
			}
			return retVal;
		}
		set => spReg = value;
	}

	[Export]
	private float mpReg = 0.1f;
	public float MPReg {
		get {
			var retVal = mpReg;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMPReg(retVal);
			}
			return retVal;
		}
		set => mpReg = value;
	}

	// Offensive
	[Export]
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

	[Export]
	private float magAtk = 5;
	public float MagAtk {
		get {
			var retVal = magAtk;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMagAtk(retVal);
			}
			return retVal;
		}
		set => magAtk = value;
	}

	[Export]
	private float acc = 100;
	public float Acc {
		get {
			var retVal = acc;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModAcc(retVal);
			}
			return retVal;
		}
		set => acc = value;
	}

	[Export]
	private float atkSpd = 3;
	public float AtkSpd {
		get {
			var retVal = atkSpd;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModAtkSpd(retVal);
			}
			return retVal;
		}
		set => atkSpd = value;
	}

	// Other
	[Export]
	private float moveSpd = 100;
	public float MoveSpd {
		get {
			var retVal = moveSpd;
			foreach (var attribute in Attributes) {
				retVal = attribute.ModMoveSpd(retVal);
			}
			return retVal;
		}
		set => moveSpd = value;
	}


	// Exp

	[Export]
	public int Lvl = 1;
	public int Exp = 0;
	public int ExpTotal = 0;
	public int ExpReq = 0;
	[Export]
	public int AP = 0;
	[Export]
	public int RP = 0;
	private int rpPotential = 0;
	[Export]
	public int RPPotential {
		get => rpPotential + (Lvl - 1);
		set => rpPotential = value;
	}
}