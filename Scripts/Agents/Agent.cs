using System;
using Godot;
using Godot.Collections;

public partial class Agent : Node2D {
	[Export]
	public Stats Stats;

	[Export]
	public Ability[] Abilities = System.Array.Empty<Ability>();

	[Export]
	public Array<PassiveAttribute> PassiveAttributes = new();
	[Export]
	public Array<PassiveEffect> PassiveEffects = new();
	[Export]
	public Array<PassiveProc> PassiveProcs = new();

	public event Action<Agent> Died;

	public float AtkTimer = 0;

	private const float CombatDistance = 100;

	public override void _Process(double delta) {
		// TODO: Improve if time
		foreach (var passiveAttr in PassiveAttributes) {
			passiveAttr.SetAttributeValues(Stats.Attributes);
		}
	}

	// fighting

	public void Attack(Agent target, double delta) {
		if (CanAttack(delta)) {
			var damage = Stats.Atk;

			damage = target.ModifyIncomingDamage(this, damage);

			target.TakeDamage(damage);

			Logger.Log($"{Name} attacked for [color=red]{damage} damage[/color]");

			foreach (var passiveProc in PassiveProcs) {
				passiveProc.Proc(this, target);
			}
		}
	}

	private float ModifyIncomingDamage(Agent attackee, float damage) {
		foreach (var PassiveEffect in PassiveEffects) {
			damage += PassiveEffect.React(this, attackee, damage);
		}
		return damage;
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
			attribute.BaseValue++;
			Logger.Log($"{Name} increased [shake rate=20.0 level=5 connected=1][color=Blue]{attribute.GetType()}[/color][/shake]");
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
		Logger.Log($"{Name} gained [color=Yellow]{amount} experience[/color]");
	}

	private void LevelUp(Stats stats) {
		stats.Lvl += 1;
		stats.ExpReq = GetReqExp(stats.Lvl + 1);

		stats.AP += 5;
		Logger.Log($"[rainbow freq=1.0 sat=0.8 val=0.8]{Name} leveld up[/rainbow]");
	}

	private int GetReqExp(int level) {
		return Convert.ToInt32(Math.Pow(level, 1.8) + level * 4);
	}
}