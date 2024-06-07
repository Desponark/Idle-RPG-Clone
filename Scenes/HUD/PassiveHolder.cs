using Godot;
using System;

public partial class PassiveHolder : HBoxContainer {
	[Export]
	private Label Lblname;
	[Export]
	private Label LblLevel;
	[Export]
	public Button Button;

	public PassiveAttribute passiveAttribute;
	public PassiveEffect passiveEffect;
	public PassiveProc passiveProc;

	public void Init(PassiveAttribute passive) {
		this.passiveAttribute = passive;
		Lblname.Text = passive.Name;
		LblLevel.Text = passive.Level.ToString();
	}
	public void Init(PassiveEffect passive) {
		this.passiveEffect = passive;
		Lblname.Text = passive.Name;
		LblLevel.Text = passive.Level.ToString();
	}
	public void Init(PassiveProc passive) {
		this.passiveProc = passive;
		Lblname.Text = passive.Name;
		LblLevel.Text = passive.Level.ToString();
	}

	public void Update() {
		var res = "";
		if (passiveAttribute != null) {
			res = passiveAttribute.Level.ToString();
		}
		else if (passiveEffect != null) {
			res = passiveEffect.Level.ToString();
		}
		else if (passiveProc != null) {
			res = passiveProc.Level.ToString();
		}
		LblLevel.Text = res;
	}
}
