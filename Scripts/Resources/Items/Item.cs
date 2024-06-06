using System;
using Godot;

[GlobalClass]
public abstract partial class Item : Resource {
	[Export]
	public string Name;
	[Export]
	public PackedScene Scene;

	public abstract event Action<Item> Used;

	public abstract void Execute(Agent agent);
}