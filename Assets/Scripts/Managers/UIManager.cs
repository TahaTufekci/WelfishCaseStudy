using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup losePanel;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private Image mainMask;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
        }

      
        private void ControlPanels(GameState gameState)
        {
            // Only fade in the main mask if the game state is Win or Lose
            if (gameState.HasFlag(GameState.Win) || gameState.HasFlag(GameState.Lose))
            {
                mainMask.DOFade(0.5f, 0.5f).SetDelay(0.8f).From(0f);
                SetMaskState(mainMask, true);
            }
            else
            {
                SetMaskState(mainMask, false);
            }

            var sequence = DOTween.Sequence();
            var delay = 0.8f;

            if (gameState.HasFlag(GameState.Lose))
            {
                sequence.PrependInterval(delay).OnComplete(() => OpenLosePanel());
            }
            else if (gameState.HasFlag(GameState.Win))
            {
                sequence.PrependInterval(delay).OnComplete(() => OpenWinPanel());
            }
        }

        private void OpenWinPanel()
        {
            winPanel.gameObject.SetActive(true);
        }
        public void OpenLosePanel()
        {
            losePanel.gameObject.SetActive(true);
        }
        private void SetMaskState(Image mask, bool isActive, Action onClickAction = null)
        {
            if (isActive)
            {
                SetMaskClickAction(mask, onClickAction);
                mask.gameObject.SetActive(true);
            }
            else
            {
                mask.gameObject.SetActive(false);
            }
        }

        private void SetMaskClickAction(Image mask, Action action)
        {
            var trigger = mask.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            trigger.triggers.Clear();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { action?.Invoke(); });
            trigger.triggers.Add(entry);
        }
        private void UpdateScoreText()
        {
           scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
        }

        private void OnEnable()
        {
            GameManager.Instance.OnEnemyDied += UpdateScoreText;
            GameManager.Instance.OnGameStateChanged += ControlPanels;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnEnemyDied -= UpdateScoreText;
            GameManager.Instance.OnGameStateChanged -= ControlPanels;
        }
    }
}