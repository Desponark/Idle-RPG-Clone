using Godot;

public partial class Player : Agent {
	[Export]
	public Inventory Inventory;

	public override void _Ready() {
		// Inventory = GetNode<Inventory>("%Inventory");
	}
}