using System;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueWrapper
    {
        public uint id;
        public string characterName;
        public string text;
        public string[] options;
        public uint[] nextWrapperID;
    }
}
