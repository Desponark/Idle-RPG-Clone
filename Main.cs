using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D {

	private List<Enemy> enemies = new();
	private Timer timer = new();
	[Export]
	private Player player;
	[Export]
	private PackedScene[] enemyScenes = new PackedScene[] { };

	public override void _Ready() {
		timer.Timeout += Timeout;
		timer.Autostart = true;
		timer.WaitTime = 3;
		AddChild(timer);
	}

	public override void _Process(double delta) {
		var firstEnemy = enemies.FirstOrDefault();
		if (firstEnemy != null && firstEnemy.Position.X <= player.Position.X + 100) {
			player.CurrentState = Player.State.Fighting;
		}

		switch (player.CurrentState) {
			case Player.State.None:
				break;
			case Player.State.Running:
				var movement = new Vector2(100, 0) * (float)delta;
				GetTree().CallGroup("Move", "Move", movement);
				break;
			case Player.State.Fighting:
				break;
			case Player.State.Resting:
				break;
		}
	}

	private void Timeout() {
		var rand = new Random();
		timer.WaitTime = rand.Next(3, 6);
		SpawnEnemy();
	}

	private void SpawnEnemy() {
		var enemyScene = enemyScenes[new Random().Next(0, enemyScenes.Count() - 1)];
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(1200, 513);
		AddChild(enemy);
		enemies.Add(enemy);
	}
}
