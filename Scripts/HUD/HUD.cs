using System;
using System.Linq;
using Godot;

public partial class HUD : CanvasLayer {
	[Export]
	private Player Player;

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

	public event Action<Attribute> AttributeUp;

	[Export]
	private PackedScene attributeControlScene;
	[Export]
	private Node attributesContainer;

	public override void _Ready() {
		PopulateAttributes();
	}

	private void PopulateAttributes() {
		foreach (var attribute in Player.Stats.Attributes) {
			var attrCtrl = attributeControlScene.Instantiate<AttributeControl>();
			attrCtrl.LbName.Text = attribute.GetType().Name;
			attrCtrl.Attribute = attribute;
			attrCtrl.LbValue.Text = attribute.Value.ToString();
			attrCtrl.Button.Pressed += () => { AttributeUp.Invoke(attribute); };
			attributesContainer.AddChild(attrCtrl);
		}
	}

	public override void _Process(double delta) {
		Update();
	}

	private void Update() {
		// attributes
		UpdateAttributes();

		// defensive
		Assign(maxHP, Player.Stats.MaxHP);
		Assign(hp, Player.Stats.HP);
		Assign(maxSP, Player.Stats.MaxSP);
		Assign(sp, Player.Stats.SP);
		Assign(maxMP, Player.Stats.MaxMP);
		Assign(mp, Player.Stats.MP);
		Assign(hpReg, Player.Stats.HPReg);
		Assign(spReg, Player.Stats.SPReg);
		Assign(mpReg, Player.Stats.MPReg);

		// offensive
		Assign(atk, Player.Stats.Atk);
		Assign(magAtk, Player.Stats.MagAtk);
		Assign(acc, Player.Stats.Acc);
		Assign(atkSpd, Player.Stats.AtkSpd);

		// other
		Assign(moveSpd, Player.Stats.MoveSpd);

		// exp
		Assign(lvl, Player.Stats.Lvl);
		Assign(exp, Player.Stats.Exp);
		Assign(expTotal, Player.Stats.ExpTotal);
		Assign(expReq, Player.Stats.ExpReq);

		Assign(ap, Player.Stats.AP);
		Assign(rp, Player.Stats.RP);
	}

	private void UpdateAttributes() {
		foreach (var node in attributesContainer.GetChildren()) {
			if (node is AttributeControl attrCtrl) {
				attrCtrl.LbValue.Text = attrCtrl.Attribute.Value.ToString();
				if (RulesetStats.CanIncreaseAttributes(Player.Stats)) {
					attrCtrl.Button.Disabled = false;
				}
				else {
					attrCtrl.Button.Disabled = true;
				}
			}
		}
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
