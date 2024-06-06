using Godot;
using System;

public partial class AttributeControl : Control {
	[Export]
	public Button button;
	[Export]
	private Label lbName;
	[Export]
	private Label lbValue;

	private Attribute attribute;

	public void Init(Attribute attribute) {
		this.attribute = attribute;

		lbName.Text = attribute.GetType().Name;
		lbValue.Text = attribute.Value.ToString();
	}

	public void Update(Agent agent) {
		lbValue.Text = attribute.Value.ToString();
		if (agent.CanIncreaseAttributes()) {
			button.Disabled = false;
		}
		else {
			button.Disabled = true;
		}
	}
}
