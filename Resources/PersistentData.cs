using Godot;

[GlobalClass]
public partial class SaveData : Resource {
	[Export]
	public int RP = 0;

	public SaveData() {
	}

	public SaveData(Stats stats) {
		this.RP = stats.RPPotential;
	}
}