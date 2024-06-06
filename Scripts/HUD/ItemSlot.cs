using Godot;
using System;

[GlobalClass]
public partial class ItemSlot : VBoxContainer {
	[Export]
	public Button useButton;
	[Export]
	private Label lblName;
	[Export]
	private TextureRect textureRect;

	public Item item;

	public virtual void Init(Item item) {
		this.item = item;

		lblName.Text = item.Name;
	}
}
