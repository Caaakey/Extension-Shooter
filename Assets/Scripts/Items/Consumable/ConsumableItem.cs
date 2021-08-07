using UnityEngine;
using UnityEngine.UI;

namespace YourName.SurvivalShooter
{
    public abstract class ConsumableItem : BaseItem
    {
        [SerializeField] private Image m_Icon;
        [SerializeField] private Text m_Text;
        private int m_Count = 0;

        public int Count
        {
            get => m_Count;
            set
            {
                if (m_Count == 99 && value > 1) return;

                m_Text.text = value.ToString();

                if (value == 0)
                {
                    m_Icon.color = new Color(1, 1, 1, 0.35f);
                    m_Text.gameObject.SetActive(false);
                }
                else
                {
                    m_Text.gameObject.SetActive(true);
                    m_Icon.color = Color.white;
                }
            }
        }

        public abstract void Use();

    }
}
