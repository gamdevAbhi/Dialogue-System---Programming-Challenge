using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "Branching Dialogue", menuName = "Branching Dialogue Object")]
    public sealed class BranchingDialogueObject : BaseDialogueObject
    {
        public string characterName = "";
        [TextArea] public string text = "";
        public List<DialogueOption> dialogueObjects;

        public override DialogueWrapper GetDialogueWrapper(uint id, Dictionary<BaseDialogueObject, DialogueWrapper> dialogueHistory)
        {
            if (dialogueHistory.ContainsKey(this)) return dialogueHistory[this];
            
            DialogueWrapper dialogueWrapper = new DialogueWrapper()
            {
                id = id,
                characterName = characterName,
                text = text,
                options = new string[dialogueObjects.Count],
                nextWrapperID = new uint[dialogueObjects.Count]
            };

            dialogueHistory.Add(this, dialogueWrapper);

            for (int i = 0; i < dialogueObjects.Count; i++)
            {
                dialogueWrapper.options[i] = dialogueObjects[i].option;

                if (dialogueObjects[i].nextDialogueObject != null)
                {
                    dialogueWrapper.nextWrapperID[i] = dialogueObjects[i].nextDialogueObject.GetDialogueWrapper(id + (uint)(i + 1), dialogueHistory).id;
                }
                else dialogueWrapper.nextWrapperID[i] = uint.MaxValue;
            }

            return dialogueWrapper;
        }
    }

    [System.Serializable]
    public class DialogueOption
    {
        public string option = "";
        public BaseDialogueObject nextDialogueObject;
    }
}
