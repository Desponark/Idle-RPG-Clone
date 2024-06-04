using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

// Gamemaster
public partial class Gamemaster : Node2D {

	public State CurrentState = State.None;
	public enum State {
		None,
		Running,
		Fighting,
		Resting
	}

	[Export]
	private Player player;
	[Export]
	private PackedScene[] enemyScenes = new PackedScene[] { };
	[Export]
	private Timer spawnTimer;
	private List<Enemy> enemies = new();

	private HUD hud;

	public override void _Ready() {
		spawnTimer.Timeout += Timeout;
		hud = GetNode<HUD>("HUD");
		// TODO: implement attributes properly
		hud.StrUp += () => { RulesetStats.IncreaseAttribute(player.Stats.Attributes.OfType<Strength>().FirstOrDefault(), player.Stats); };
		hud.DexUp += () => { RulesetStats.IncreaseAttribute("dex", player.Stats); };
		hud.IntUp += () => { RulesetStats.IncreaseAttribute("int", player.Stats); };
	}

	public override void _Input(InputEvent @event) {
		if ((@event is InputEventKey || @event is InputEventMouseButton) && @event.IsPressed()) {
			if (CurrentState == State.None) CurrentState = State.Running;
		}
	}

	public override void _Process(double delta) {
		hud.Update(player);

		var enemy = enemies.FirstOrDefault();
		if (Ruleset.IsInCombatDistance(player, enemy)) {
			CurrentState = State.Fighting;
		}

		switch (CurrentState) {
			case State.None:
				spawnTimer.Paused = true;
				break;
			case State.Running:
				spawnTimer.Paused = false;
				AdvanceWorld(delta);
				break;
			case State.Fighting:
				spawnTimer.Paused = true;

				Ruleset.Combat(player, enemy, delta);

				if (Ruleset.IsDead(enemy)) {
					RulesetStats.GainExp(player.Stats, enemy.ExpWorth);
					enemies.Remove(enemy);
					enemy.QueueFree();
					CurrentState = State.Running;
				}
				if (Ruleset.IsDead(player)) {
					// restart game
					GetTree().ReloadCurrentScene();
				}
				break;
			case State.Resting:
				spawnTimer.Paused = true;
				break;
		}
	}

	private void AdvanceWorld(double delta) {
		var movement = new Vector2(100, 0) * (float)delta;
		GetTree().CallGroup("Move", "Move", movement);
	}

	private void Timeout() {
		var rand = new Random();
		spawnTimer.WaitTime = rand.Next(3, 6);
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
