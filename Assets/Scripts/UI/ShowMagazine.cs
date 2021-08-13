using UnityEngine;
using UnityEngine.UI;

namespace YourName.SurvivalShooter.UI
{
    [RequireComponent(typeof(Text))]
    public class ShowMagazine : MonoBehaviour
    {
        private Text m_Text;

        private void Awake()
        {
            m_Text = GetComponent<Text>();
        }

        public string TextString { get => m_Text.text; set => m_Text.text = value; }

    }
}
