using Godot;
using System;

public partial class AttributeControl : Control {
	[Export]
	public Button Button;
	[Export]
	public Label LbName;
	[Export]
	public Label LbValue;

	public Attribute Attribute;

	public void Display(Attribute attribute) {
		LbName.Text = attribute.GetType().Name;
		Attribute = attribute;
		LbValue.Text = attribute.Value.ToString();
	}
}
