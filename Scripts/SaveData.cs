using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class SaveData : Resource {
	private const string FILENAME = "SaveData.tres";

	[Export]
	private int RP = 0;

	// Gamestatistics
	[Export]
	private int HighestLevelRecord = 0;
	[Export]
	private float LongestRunRecord = 0;
	[Export]
	private float TimeRunningRecord = 0;
	[Export]
	private int MonstersKilledRecord = 0;

	[Export]
	private Array<string> Log;

	public SaveData() {
	}

	public SaveData(Stats stats, GameStatistics gameStatistics) {
		SetStats(stats);
		SetStatistics(gameStatistics);
	}

	private void SetStats(Stats stats) {
		this.RP = stats.RPPotential;
	}

	private void SetStatistics(GameStatistics gameStatistics) {
		if (gameStatistics.HighestLevel > HighestLevelRecord)
			HighestLevelRecord = gameStatistics.HighestLevel;

		if (gameStatistics.LongestRun > LongestRunRecord)
			LongestRunRecord = gameStatistics.LongestRun;

		if (gameStatistics.TimeRunning > TimeRunningRecord)
			TimeRunningRecord = gameStatistics.TimeRunning;

		if (gameStatistics.MonstersKilled > MonstersKilledRecord)
			MonstersKilledRecord = gameStatistics.MonstersKilled;
	}

	private void SetLog(List<string> lines) {
		Log = new Array<string>(lines);
	}

	public static void Save(Node anyNode) {
		var data = new SaveData();

		var nodes = anyNode.GetTree().GetNodesInGroup("persist");
		foreach (var node in nodes) {
			if (node is Player playr) {
				data.SetStats(playr.Stats);
			}
			else if (node is Gamemaster gamemaster) {
				data.SetStatistics(gamemaster.gameStatistics);
			}
			else if (node is Logger logger) {
				data.SetLog(logger.TakeLastRawLines(10));
			}
		}

		ResourceSaver.Save(data, FILENAME);
	}

	public static void Load(Node anyNode) {
		if (ResourceLoader.Exists(FILENAME)) {
			var resource = ResourceLoader.Load(FILENAME);
			if (resource is SaveData saveData) {
				var nodes = anyNode.GetTree().GetNodesInGroup("persist");

				Logger.Log("[color=red]----------Previously----------[/color]");
				foreach (var node in nodes) {
					if (node is Player playr) {
						playr.Stats.RP = saveData.RP;
					}
					else if (node is Gamemaster gamemaster) {
						gamemaster.gameStatistics.HighestLevelRecord = saveData.HighestLevelRecord;
						gamemaster.gameStatistics.LongestRunRecord = saveData.LongestRunRecord;
						gamemaster.gameStatistics.TimeRunningRecord = saveData.TimeRunningRecord;
						gamemaster.gameStatistics.MonstersKilledRecord = saveData.MonstersKilledRecord;
					}
					else if (node is Logger logger) {
						foreach (var item in saveData.Log) {
							Logger.Log(item);
						}
					}
				}
				Logger.Log("[color=blue]----------Now----------[/color]");
			}
		}
	}

	public static void Save(Agent player, GameStatistics gameStatistics) {
		var data = new SaveData(player.Stats, gameStatistics);
		ResourceSaver.Save(data, FILENAME);
	}

	public static void Load(Player player, GameStatistics gameStatistics) {
		if (ResourceLoader.Exists(FILENAME)) {
			var resource = ResourceLoader.Load(FILENAME);
			if (resource is SaveData saveData) {
				player.Stats.RP = saveData.RP;

				gameStatistics.HighestLevelRecord = saveData.HighestLevelRecord;
				gameStatistics.LongestRunRecord = saveData.LongestRunRecord;
				gameStatistics.TimeRunningRecord = saveData.TimeRunningRecord;
				gameStatistics.MonstersKilledRecord = saveData.MonstersKilledRecord;
			}
		}
	}
}