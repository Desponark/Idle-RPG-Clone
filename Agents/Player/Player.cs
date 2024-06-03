using Godot;
using System;

public partial class Player : Sprite2D {
	private Attributes attributes = new();
	private Stats stats = new();

	public State CurrentState;

	public enum State {
		None,
		Resting,
		Running,
		Fighting
	}

	public override void _Ready() {
	}

	public override void _Process(double delta) {
	}
}

public class Attributes {
	private int strength = 1;
	private int dexterity = 1;
	private int intelligence = 1;
}

public class Stats {
	private float hp;
	private float sp;
	private float mp;
}