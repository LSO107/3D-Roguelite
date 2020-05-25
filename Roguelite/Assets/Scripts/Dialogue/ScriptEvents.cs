using Player;
using ScriptingFramework;

namespace Dialogue
{
    internal sealed class ScriptEvents : IEventProvider
    {
        public void StartDay()
        {
            DayNightCycle.Instance.StartNewDay();
            NpcFight.Instance.BeginNpcFight(PlayerManager.Instance.PlayerStats.CombatLevel);
        }
    }
}
