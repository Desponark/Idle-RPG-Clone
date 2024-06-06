using Godot;
using System;

public partial class ItemSlotHpPotion : ItemSlot {
	[Export]
	private Label lblHealAmount;

	public override void Init(Item item) {
		base.Init(item);

		if (item is HpPotion hpPotion) {
			lblHealAmount.Text = hpPotion.HealAmount.ToString();
		}
	}
}
