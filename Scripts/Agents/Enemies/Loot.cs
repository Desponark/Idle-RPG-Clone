using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Loot : Resource {
	[Export]
	public Array<Item> Items = new();

	[Export]
	public float Dropchance = 10;
}