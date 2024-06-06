using Godot;
using System;

public partial class ItemSlot : VBoxContainer {
	[Export]
	public Button useButton;
	[Export]
	private Label lblName;
	[Export]
	private TextureRect textureRect;

	public Item item;

	public void Init(Item item) {
		this.item = item;

		lblName.Text = item.Name;
	}
}
