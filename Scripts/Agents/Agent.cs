using System;
using Godot;

public partial class Agent : Node2D {
	[Export]
	public Stats Stats;

	[Export]
	public Ability[] Abilities = System.Array.Empty<Ability>();

	public event Action<Agent> Died;

	public float AtkTimer = 0;

	private const float CombatDistance = 100;

	// fighting
	public void Attack(Agent target, double delta) {
		if (CanAttack(delta)) {
			target.TakeDamage(Stats.Atk);
		}
	}

	public bool IsInCombatDistance(Agent enemy) {
		if (enemy == null)
			return false;

		return enemy.Position.X <= Position.X + CombatDistance;
	}

	private bool CanAttack(double delta) {
		if (AtkTimer > 0) {
			AtkTimer -= (float)delta;
		}
		if (AtkTimer <= 0) {
			AtkTimer = Stats.AtkSpd;
			return true;
		}
		return false;
	}

	// health
	public void HealHp(int amount) {
		Stats.HP += amount;
		if (Stats.HP > Stats.MaxHP)
			Stats.HP = Stats.MaxHP;
	}

	public void TakeDamage(float amount) {
		Stats.HP -= amount;

		if (IsDead())
			Died.Invoke(this);
	}

	public bool IsDead() {
		return Stats.HP <= 0;
	}

	// Attributes
	public bool CanIncreaseAttributes() {
		return Stats.AP > 0;
	}

	public void IncreaseAttribute(Attribute attribute) {
		if (CanIncreaseAttributes()) {
			Stats.AP--;
			attribute.Value++;
		}
	}

	// Exp
	public void GainExp(int amount) {
		Stats.ExpTotal += amount;
		Stats.Exp += amount;
		while (Stats.Exp >= Stats.ExpReq) {
			Stats.Exp -= Stats.ExpReq;
			LevelUp(Stats);
		}
	}

	private void LevelUp(Stats stats) {
		stats.Lvl += 1;
		stats.ExpReq = GetReqExp(stats.Lvl + 1);

		stats.AP += 5;
	}

	private int GetReqExp(int level) {
		return Convert.ToInt32(Math.Pow(level, 1.8) + level * 4);
	}
}