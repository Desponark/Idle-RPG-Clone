using Godot;

public partial class AbilityButton : VBoxContainer {
	[Export]
	public TextureButton UseButton;
	[Export]
	public Button UpgradeButton;
	[Export]
	public Label LblName;
	[Export]
	public Label LblLevel;
	[Export]
	public Label LblCost;
	[Export]
	public Label LblDamage;
	[Export]
	public Label LblUpgradeCost;

	public void Display(Ability ability) {
		LblName.Text = ability.Name;
		LblLevel.Text = ability.Level.ToString();
		LblCost.Text = ability.CastCost.ToString();
		LblDamage.Text = ability.BaseDamage.ToString();
		LblUpgradeCost.Text = ability.UpgradeCost.ToString();
	}
}
