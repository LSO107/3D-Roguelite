using UnityEngine;

namespace Character.Combat
{
    internal sealed class CharacterCombat : MonoBehaviour
    {
        public CombatState CombatState;

        public void UpdateState(CombatState state)
        {
            CombatState = state;
        }
    }
}
