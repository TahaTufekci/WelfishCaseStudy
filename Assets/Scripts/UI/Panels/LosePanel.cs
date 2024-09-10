using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels
{
    public class LosePanel : MonoBehaviour
    {
        public void TryAgain()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            DOTween.KillAll();
            Application.Quit();
            Debug.Log("Quit");
        }

        public void OnEnable()
        {
            EnableAnimation();
        }

        private void EnableAnimation()
        {
            gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
        }
    }
}
