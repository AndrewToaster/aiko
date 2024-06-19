using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleInstance
{
	public BattleEnemyData enemy;
	public Type scriptType;
	public string splashText;
	public BattleState state;
	public object context;

	public BattleInstance(BattleEnemyData enemy, string splashText, Type scriptType, object context = null)
	{
		this.enemy = enemy;
		this.splashText = splashText;
		this.scriptType = scriptType;
		this.context = context;
    }
}
