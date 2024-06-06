using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Inventory : Node {
	[Export]
	private Array<Item> items = new Array<Item>();

	public event Action<Item> Added;
	public event Action<Item> Removed;

	public override void _Ready() {
		foreach (var item in items) {
			item.Used -= RemoveItem;
			item.Used += RemoveItem;
		}
	}

	public void AddItem(Item item) {
		items.Add(item);

		item.Used -= RemoveItem;
		item.Used += RemoveItem;

		Added.Invoke(item);
	}

	public void RemoveItem(Item item) {
		items.Remove(item);

		Removed.Invoke(item);
	}

	public Array<Item> GetItems() {
		return items;
	}
}