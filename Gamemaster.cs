using Godot;
using Godot.Collections;
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
	private Array<PackedScene> enemyScenes = new();
	[Export]
	private Timer spawnTimer;
	private List<Enemy> enemies = new();

	private GameStatistics gameStatistics = new();

	private Random random = new();

	public override void _Ready() {
		spawnTimer.Timeout += Timeout;

		SaveData.Load(player, gameStatistics);
		player.Died += OnPlayerDeath;

		hud.Setup(gameStatistics);
		hud.AttributeUp += (Attribute attribute) => { player.IncreaseAttribute(attribute); };
		hud.AbilityUpgrade += (Ability ability) => { ability.Upgrade(player); };
		hud.AbilityUse += (Ability ability) => { ability.Execute(player, enemies.FirstOrDefault()); };
		hud.ItemUse += (Item item) => { item.Execute(player); };
	}

	public override void _Input(InputEvent @event) {
		if ((@event is InputEventKey || @event is InputEventMouseButton) && @event.IsPressed()) {
			if (CurrentState == State.None) CurrentState = State.Running;
		}
	}

	public override void _Process(double delta) {
		var enemy = enemies.FirstOrDefault();
		if (player.IsInCombatDistance(enemy)) {
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

				player.Attack(enemy, delta);
				enemy.Attack(player, delta);
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
		var enemyScene = enemyScenes[random.Next(0, enemyScenes.Count)];
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(1200, 513);
		AddChild(enemy, true);
		enemies.Add(enemy);

		enemy.Died += OnEnemyDeath;
	}

	private void OnEnemyDeath(Agent agent) {
		if (agent is Enemy enemy) {
			player.GainExp(enemy.ExpWorth);
			enemy.DropLoot(player);

			enemies.Remove(enemy);
			enemy.QueueFree();
			CurrentState = State.Running;

			gameStatistics.MonstersKilled++;
		}
	}

	private void OnPlayerDeath(Agent agent) {
		// restart game
		SaveData.Save(agent, gameStatistics);
		GetTree().ReloadCurrentScene();
	}
}
