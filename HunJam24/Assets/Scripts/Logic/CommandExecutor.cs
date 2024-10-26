using System;
using Logic.Characters;

namespace Logic
{
    public static class CommandExecutor
    {
        public static bool Execute(Func<Character, bool> action)
        {
            if (!action.Invoke(MapManager.Instance.Player))
            {
                return false;
            }

            CloneManager.Instance.UpdateHistory(action.Invoke);

            return true;
        }
    }
}