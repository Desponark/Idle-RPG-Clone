using Godot;
using System;

public partial class PointsBar : ProgressBar {
	[Export]
	private Stats stats;

	private Label lblPoints;

	private Label lblName;
	[Export]
	private BarType barType = BarType.HP;

	private enum BarType {
		HP,
		SP,
		MP
	}

	public override void _Ready() {
		lblPoints = GetNode<Label>("%Points");
		lblName = GetNode<Label>("%Name");
	}

	public override void _Process(double delta) {
		switch (barType) {
			case BarType.HP:
				MaxValue = stats.MaxHP;
				Value = stats.HP;
				lblName.Text = barType.ToString();
				break;
			case BarType.SP:
				MaxValue = stats.MaxSP;
				Value = stats.SP;
				lblName.Text = barType.ToString();
				break;
			case BarType.MP:
				MaxValue = stats.MaxMP;
				Value = stats.MP;
				lblName.Text = barType.ToString();
				break;
		}

		lblPoints.Text = Value.ToString() + "/" + MaxValue.ToString();
	}
}
