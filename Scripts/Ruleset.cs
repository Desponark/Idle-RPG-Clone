using System;

public static class Ruleset {

	private const float CombatDistance = 100;

	public static void Attack(Agent attacker, Agent target, double delta) {
		if (CanAttack(attacker, delta)) {

			TakeDamage(target, RulesetStats.GetAtk(attacker.Stats));
		}
	}

	private static void TakeDamage(Agent target, float amount) {
		target.Stats.HP -= amount;
	}

	private static bool CanAttack(Agent attacker, double delta) {
		if (attacker.AtkTimer > 0) {
			attacker.AtkTimer -= (float)delta;
		}
		if (attacker.AtkTimer <= 0) {
			attacker.AtkTimer = RulesetStats.GetAtkSpd(attacker.Stats);
			return true;
		}
		return false;
	}

	public static void Combat(Agent player, Agent enemy, double delta) {
		Ruleset.Attack(enemy, player, delta);
		Ruleset.Attack(player, enemy, delta);
	}

	public static bool IsDead(Agent agent) {
		return agent.Stats.HP <= 0;
	}

	public static bool IsInCombatDistance(Agent player, Agent enemy) {
		if (enemy == null)
			return false;

		return enemy.Position.X <= player.Position.X + CombatDistance;
	}
}

public static class RulesetStats {
	// Stats - Offensive
	public static float GetAtk(Stats stats) {
		return stats.Atk + stats.Strength;
	}

	public static float GetMagAtk(Stats stats) {
		return stats.MagAtk + stats.Intelligence;
	}

	public static float GetAtkSpd(Stats stats) {
		return stats.AtkSpd - (stats.Dexterity / 100);
	}

	public static bool CanIncreaseAttributes(Stats stats) {
		return stats.AP > 0;
	}

	public static void IncreaseAttribute(Attribute attribute, Stats stats) {
		if (CanIncreaseAttributes(stats)) {
			attribute.Value += 1;
		}
	}

	public static void IncreaseAttribute(string attribute, Stats stats) {
		if (CanIncreaseAttributes(stats)) {
			if (attribute == "str") {
				stats.AP--;
				stats.Strength += 1;
			}
			else if (attribute == "dex") {
				stats.AP--;
				stats.Dexterity += 1;
			}
			else if (attribute == "int") {
				stats.AP--;
				stats.Intelligence += 1;
			}
		}
	}

	// Exp
	public static void GainExp(Stats stats, int amount) {
		stats.ExpTotal += amount;
		stats.Exp += amount;
		while (stats.Exp >= stats.ExpReq) {
			stats.Exp -= stats.ExpReq;
			LevelUp(stats);
		}
	}

	private static void LevelUp(Stats stats) {
		stats.Lvl += 1;
		stats.ExpReq = GetReqExp(stats.Lvl + 1);

		stats.AP += 5;
	}

	private static int GetReqExp(int level) {
		return Convert.ToInt32(Math.Pow(level, 1.8) + level * 4);
	}
}