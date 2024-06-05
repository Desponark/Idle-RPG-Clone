using Godot;

[GlobalClass]
public partial class SaveData : Resource {
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
}