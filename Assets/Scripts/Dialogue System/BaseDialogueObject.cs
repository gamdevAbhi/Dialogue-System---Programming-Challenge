using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public abstract class BaseDialogueObject : ScriptableObject
    {
        public abstract DialogueWrapper GetDialogueWrapper(uint id, Dictionary<BaseDialogueObject, DialogueWrapper> dialogueHistory);
    }
}
