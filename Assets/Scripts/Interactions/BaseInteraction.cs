using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace YourName.SurvivalShooter.Interactions
{
    public abstract class BaseInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Image m_SpeechBubble;
        private IEnumerator m_CurrentUpdate = null;
        protected bool IsEnterPlayer = false;

        private float BubbleAlpha
        {
            get => m_SpeechBubble.color.a;
            set
            {
                m_SpeechBubble.color = new Color(1, 1, 1, value);
            }
        }

        protected IEnumerator CurrentUpdator
        {
            get => m_CurrentUpdate;
            set
            {
                if (m_CurrentUpdate != null)
                    StopCoroutine(m_CurrentUpdate);

                m_CurrentUpdate = value;
                if (value != null)
                    StartCoroutine(m_CurrentUpdate);
            }
        }

        protected virtual void Update()
        {
            if (IsEnterPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out var hit, Mathf.Infinity, GameManager.Get.NPCLayerMask))
                    {
                        if (hit.collider.gameObject.name == "NPC")
                            StartInteraction();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            IsEnterPlayer = true;
            CurrentUpdator = UpdateEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            IsEnterPlayer = false;
            CurrentUpdator = UpdateExit();
        }

        protected virtual IEnumerator UpdateEnter()
        {
            while (BubbleAlpha <= 1f)
            {
                BubbleAlpha += Time.deltaTime * 2f;
                if (BubbleAlpha >= 1f)
                {
                    BubbleAlpha = 1;
                    yield break;
                }

                yield return null;
            }

            yield break;
        }

        protected virtual IEnumerator UpdateExit()
        {
            while (BubbleAlpha >= 0f)
            {
                BubbleAlpha -= Time.deltaTime * 2f;
                if (BubbleAlpha <= 0f)
                {
                    BubbleAlpha = 0;
                    yield break;
                }

                yield return null;
            }

            yield break;
        }

        protected abstract void StartInteraction();
    }
}
