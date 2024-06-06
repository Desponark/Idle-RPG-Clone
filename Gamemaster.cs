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
	private HUD hud;
	[Export]
	private PackedScene[] enemyScenes = new PackedScene[] { };
	[Export]
	private Timer spawnTimer;
	private List<Enemy> enemies = new();

	private GameStatistics gameStatistics = new();

	public override void _Ready() {
		spawnTimer.Timeout += Timeout;

		RulesetStats.Load(player, gameStatistics);

		hud.Setup(gameStatistics);
		hud.AttributeUp += (Attribute attribute) => { RulesetStats.IncreaseAttribute(attribute, player.Stats); };
		hud.AbilityUpgrade += (Ability ability) => { ability.Upgrade(player); };
		hud.AbilityUse += (Ability ability) => { ability.Execute(player, enemies.FirstOrDefault()); };
	}

	public override void _Input(InputEvent @event) {
		if ((@event is InputEventKey || @event is InputEventMouseButton) && @event.IsPressed()) {
			if (CurrentState == State.None) CurrentState = State.Running;
		}
	}

	public override void _Process(double delta) {
		var enemy = enemies.FirstOrDefault();
		if (RulesetCombat.IsInCombatDistance(player, enemy)) {
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

				RulesetCombat.Combat(player, enemy, delta);

				if (RulesetCombat.IsDead(enemy)) {
					RulesetStats.GainExp(player.Stats, enemy.ExpWorth);
					enemies.Remove(enemy);
					enemy.QueueFree();
					CurrentState = State.Running;

					gameStatistics.MonstersKilled++;
				}
				if (RulesetCombat.IsDead(player)) {
					// restart game
					RulesetStats.Save(player, gameStatistics);
					GetTree().ReloadCurrentScene();
				}
				break;
			case State.Resting:
				spawnTimer.Paused = true;
				break;
		}

		gameStatistics.HighestLevel = player.Stats.Lvl;
		gameStatistics.TimeRunning += (float)delta;
	}

	private void AdvanceWorld(double delta) {
		var speed = player.Stats.MoveSpd;
		var movement = new Vector2(speed, 0) * (float)delta;
		GetTree().CallGroup("Move", "Move", movement);

		gameStatistics.LongestRun += speed * (float)delta;
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
