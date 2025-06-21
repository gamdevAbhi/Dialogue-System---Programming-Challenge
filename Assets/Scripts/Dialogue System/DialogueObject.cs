using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue Object")]
    public sealed class DialogueObject : BaseDialogueObject
    {
        public string characterName = "";
        [TextArea] public string text = "";
        public BaseDialogueObject nextDialogueObject;

        public override DialogueWrapper GetDialogueWrapper(uint id, Dictionary<BaseDialogueObject, DialogueWrapper> dialogueHistory)
        {
            if (dialogueHistory.ContainsKey(this)) return dialogueHistory[this];

            DialogueWrapper dialogueWrapper = new DialogueWrapper
            {
                id = id,
                characterName = characterName,
                text = text,
                options = null
            };

            dialogueHistory.Add(this, dialogueWrapper);

            if (nextDialogueObject != null) dialogueWrapper.nextWrapperID = new uint[1] { nextDialogueObject.GetDialogueWrapper(id + 1, dialogueHistory).id };
            else dialogueWrapper.nextWrapperID = null;

            return dialogueWrapper;
        }
    }
}
