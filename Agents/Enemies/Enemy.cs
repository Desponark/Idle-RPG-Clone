using Godot;

public partial class Enemy : Agent {
	public int ExpWorth = 10;

	public override void _Ready() {
		AddToGroup("Move", true);
		Stats.HP = 10;
	}

	public void Move(Vector2 movement) {
		Position -= movement;
	}
}
