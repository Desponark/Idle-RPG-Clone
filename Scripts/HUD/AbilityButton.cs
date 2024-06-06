using Godot;

public partial class AbilityButton : VBoxContainer {
	[Export]
	public TextureButton UseButton;
	[Export]
	public Button UpgradeButton;
	[Export]
	private Label LblName;
	[Export]
	private Label lblLevel;
	[Export]
	private Label lblCost;
	[Export]
	private Label lblDamage;
	[Export]
	private Label lblUpgradeCost;

	private Ability ability;

	public void Init(Ability ability) {
		this.ability = ability;

		LblName.Text = ability.Name;
		UpdateLabels();
	}

	public void Update() {
		UpdateLabels();

		if (ability.IsUnlocked()) {
			UseButton.Disabled = false;
		}
		else {
			UseButton.Disabled = true;
		}
	}

	private void UpdateLabels() {
		lblLevel.Text = ability.Level.ToString();
		lblCost.Text = ability.CastCost.ToString();
		lblDamage.Text = ability.BaseDamage.ToString();
		lblUpgradeCost.Text = ability.UpgradeCost.ToString();
	}
}
