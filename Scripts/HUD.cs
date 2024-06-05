using System;
using System.Linq;
using Godot;

public partial class HUD : CanvasLayer {
	[Export]
	private Label strength, dexterity, intelligence;
	[Export]
	private Label maxHP, hp, maxSP, sp, maxMP, mp, hpReg, spReg, mpReg;
	[Export]
	private Label atk, magAtk, acc, atkSpd;
	[Export]
	private Label moveSpd;
	[Export]
	private Label lvl, exp, expTotal, expReq;
	[Export]
	private Label ap, rp;

	[Export]
	private Button btnStr, btnDex, btnInt;

	public event Action StrUp, DexUp, IntUp;

	public override void _Ready() {
		btnStr.Pressed += () => { StrUp.Invoke(); };
		btnDex.Pressed += () => { DexUp.Invoke(); };
		btnInt.Pressed += () => { IntUp.Invoke(); };
	}

	public void Update(Player player) {
		if (RulesetStats.CanIncreaseAttributes(player.Stats)) {
			btnStr.Disabled = false;
			btnDex.Disabled = false;
			btnInt.Disabled = false;
		}
		else {
			btnStr.Disabled = true;
			btnDex.Disabled = true;
			btnInt.Disabled = true;
		}

		// attributes
		Assign(strength, player.Stats.Attributes.OfType<Strength>().FirstOrDefault().Value);
		Assign(dexterity, player.Stats.Attributes.OfType<Dexterity>().FirstOrDefault().Value);
		Assign(intelligence, player.Stats.Attributes.OfType<Intelligence>().FirstOrDefault().Value);

		// defensive
		Assign(maxHP, player.Stats.MaxHP);
		Assign(hp, player.Stats.HP);
		Assign(maxSP, player.Stats.MaxSP);
		Assign(sp, player.Stats.SP);
		Assign(maxMP, player.Stats.MaxMP);
		Assign(mp, player.Stats.MP);
		Assign(hpReg, player.Stats.HPReg);
		Assign(spReg, player.Stats.SPReg);
		Assign(mpReg, player.Stats.MPReg);

		// offensive
		Assign(atk, player.Stats.Atk);
		Assign(magAtk, player.Stats.MagAtk);
		Assign(acc, player.Stats.Acc);
		Assign(atkSpd, player.Stats.AtkSpd);

		// other
		Assign(moveSpd, player.Stats.MoveSpd);

		// exp
		Assign(lvl, player.Stats.Lvl);
		Assign(exp, player.Stats.Exp);
		Assign(expTotal, player.Stats.ExpTotal);
		Assign(expReq, player.Stats.ExpReq);

		Assign(ap, player.Stats.AP);
		Assign(rp, player.Stats.RP);
	}

	private static void Assign(Label label, float stat) {
		if (label != null)
			label.Text = stat.ToString();
	}

	private static void Assign(Label label, int stat) {
		if (label != null)
			label.Text = stat.ToString();
	}
}
