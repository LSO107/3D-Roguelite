using System.Linq;
using ScriptingFramework;

namespace Dialogue
{
    internal sealed class ScriptEvents : IEventProvider
    {
        public void StartDay()
        {
            DayNightCycle.Instance.StartNewDay();
        }

        public void RegisterGuideEvents()
        {
            var npcManager = NpcManager.Instance;

            var guide = npcManager.GetNpc(3);
            var guideOriginalPosition = guide.transform.position;

            var tempLocation = new UnityEngine.Vector3(1000, 1000, 1000);

            DayNightCycle.Instance.RegisterStartOfDayEvent(() => npcManager.TeleportNpc(guide, tempLocation, 1));
            DayNightCycle.Instance.RegisterEndOfDayEvent(() => npcManager.TeleportNpc(guide, guideOriginalPosition));
        }
    }
}
