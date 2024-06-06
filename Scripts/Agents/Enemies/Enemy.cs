using System;
using Godot;
using Godot.Collections;

public partial class Enemy : Agent {
	[Export]
	public int ExpWorth = 10;

	[Export]
	public Array<Loot> Loot = new();

	public override void _Ready() {
		AddToGroup("Move", true);
		Stats.HP = 10;
	}

	public void Move(Vector2 movement) {
		Position -= movement;
	}

	public void DropLoot(Player player) {
		var random = new Random();
		foreach (var item in Loot) {
			if (item.Dropchance >= random.Next(100)) {
				GD.Print("Player dropped item(s)");
				foreach (var it in item.Items) {
					player.Inventory.AddItem(it);
				}
			}
		}
	}
}
