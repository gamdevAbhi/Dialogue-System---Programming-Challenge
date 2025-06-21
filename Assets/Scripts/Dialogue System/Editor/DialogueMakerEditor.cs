using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

namespace DialogueSystem.Editor
{
    public static class DialogueMaker
    {
        [MenuItem(itemName: "Assets/Serialize Dialogue", isValidateFunction: true, priority: 1)]
        public static bool ValidateSerializeDialogue()
        {
            if (Selection.activeObject == null || Selection.activeObject is BaseDialogueObject == false ||
            Selection.objects.Length > 1) return false;
            else return true;
        }

        [MenuItem(itemName: "Assets/Serialize Dialogue", isValidateFunction: false, priority: 1)]
        public static void SerializeDialogue()
        {
            Dictionary<BaseDialogueObject, DialogueWrapper> dialogueHistory = new Dictionary<BaseDialogueObject, DialogueWrapper>();

            (Selection.activeObject as BaseDialogueObject).GetDialogueWrapper(0, dialogueHistory);

            DialogueWrappersHolder dialogueWrappersHolder = new DialogueWrappersHolder()
            {
                dialogueWrappers = dialogueHistory.Values.ToArray()
            };

            string json = JsonUtility.ToJson(dialogueWrappersHolder, true);

            string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string folderPath = Path.GetDirectoryName(selectedPath).Replace("\\", "/");
            string filePath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/dialogueWrapper.json");

            File.WriteAllText(filePath, json);

            AssetDatabase.Refresh();
        }

        [Serializable]
        public class DialogueWrappersHolder
        {
            public DialogueWrapper[] dialogueWrappers;
        }
    }
}