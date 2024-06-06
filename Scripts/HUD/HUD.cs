using System;
using Godot;

public partial class HUD : CanvasLayer {
	public event Action<Attribute> AttributeUp;
	public event Action<Ability> AbilityUpgrade;
	public event Action<Ability> AbilityUse;

	[Export]
	private Player player;
	private GameStatistics gameStatistics;

	[Export]
	private Label maxHP, hp, maxSP, sp, maxMP, mp, hpReg, spReg, mpReg;
	[Export]
	private Label atk, magAtk, acc, atkSpd;
	[Export]
	private Label moveSpd;
	[Export]
	private Label lvl, exp, expTotal, expReq;
	[Export]
	private Label ap, rp, rpPotential;

	[Export]
	private Label highestLevel, longestRun, timeRunning, monstersKilled;
	[Export]
	private Label highestLevelRecord, longestRunRecord, timeRunningRecord, monstersKilledRecord;

	[Export]
	private Node attributesContainer;

	[Export]
	private Node abilityContainer;


	public void Setup(GameStatistics gameStatistics) {
		this.gameStatistics = gameStatistics;
	}

	public override void _Ready() {
		PopulateAttributes();
		PopulateAbilities();
	}

	private void PopulateAttributes() {
		foreach (var attribute in player.Stats.Attributes) {
			var attrCtrl = attribute.Scene.Instantiate<AttributeControl>();
			attributesContainer.AddChild(attrCtrl);
			attrCtrl.Init(attribute);
			attrCtrl.button.Pressed += () => { AttributeUp.Invoke(attribute); };
		}
	}

	private void PopulateAbilities() {
		foreach (var ability in player.Abilities) {
			var abilityBtn = ability.Scene.Instantiate<AbilityButton>();
			abilityContainer.AddChild(abilityBtn);
			abilityBtn.Init(ability);
			abilityBtn.UseButton.Pressed += () => { AbilityUse.Invoke(ability); };
			abilityBtn.UpgradeButton.Pressed += () => { AbilityUpgrade.Invoke(ability); };
		}
	}

	public override void _Process(double delta) {
		// attributes
		UpdateAttributes();

		UpdateAbilities();

		UpdatePlayerStats();

		UpdateGameStatistics();
	}

	private void UpdateAttributes() {
		foreach (var node in attributesContainer.GetChildren()) {
			if (node is AttributeControl attrCtrl) {
				attrCtrl.Update(player.Stats);
			}
		}
	}

	private void UpdateAbilities() {
		foreach (var node in abilityContainer.GetChildren()) {
			if (node is AbilityButton abilityButton) {
				abilityButton.Update();
			}
		}
	}

	private void UpdatePlayerStats() {
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
		Assign(rpPotential, player.Stats.RPPotential);
	}

	private void UpdateGameStatistics() {
		Assign(highestLevel, gameStatistics.HighestLevel);
		Assign(longestRun, gameStatistics.LongestRun);
		Assign(timeRunning, TimeSpan.FromSeconds(gameStatistics.TimeRunning));
		Assign(monstersKilled, gameStatistics.MonstersKilled);

		Assign(highestLevelRecord, gameStatistics.HighestLevelRecord);
		Assign(longestRunRecord, gameStatistics.LongestRunRecord);
		Assign(timeRunningRecord, TimeSpan.FromSeconds(gameStatistics.TimeRunningRecord));
		Assign(monstersKilledRecord, gameStatistics.MonstersKilledRecord);
	}


	private static void Assign(Label label, float stat) {
		if (label != null)
			label.Text = stat.ToString("0.00");
	}

	private static void Assign(Label label, int stat) {
		if (label != null)
			label.Text = stat.ToString();
	}

	private static void Assign(Label label, TimeSpan span) {
		if (label != null)
			label.Text = span.ToString(@"hh\:mm\:ss\:ff");
	}
}
