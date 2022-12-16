using UnityEngine;

namespace SnekTech.UI.Modal
{
    public class ModalContent
    {
        public readonly string HeaderText;
        public readonly GameObject Body;

        public ModalContent(string headerText, GameObject body)
        {
            HeaderText = headerText;
            Body = body;
        }
    }
}
