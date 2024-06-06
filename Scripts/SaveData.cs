using Godot;

[GlobalClass]
public partial class SaveData : Resource {
	private const string FILENAME = "SaveData.tres";

	[Export]
	public int RP = 0;

	// Gamestatistics
	[Export]
	public int HighestLevelRecord = 0;
	[Export]
	public float LongestRunRecord = 0;
	[Export]
	public float TimeRunningRecord = 0;
	[Export]
	public int MonstersKilledRecord = 0;


	public SaveData() {
	}

	public SaveData(Stats stats, GameStatistics gameStatistics) {
		this.RP = stats.RPPotential;

		if (gameStatistics.HighestLevel > HighestLevelRecord)
			HighestLevelRecord = gameStatistics.HighestLevel;

		if (gameStatistics.LongestRun > LongestRunRecord)
			LongestRunRecord = gameStatistics.LongestRun;

		if (gameStatistics.TimeRunning > TimeRunningRecord)
			TimeRunningRecord = gameStatistics.TimeRunning;

		if (gameStatistics.MonstersKilled > MonstersKilledRecord)
			MonstersKilledRecord = gameStatistics.MonstersKilled;
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