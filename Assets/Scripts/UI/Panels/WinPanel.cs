using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels
{
    public class WinPanel : MonoBehaviour
    {
        public void PlayAgain()
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

        private void EnableAnimation()
        {
            gameObject.transform.DOScale(Vector3.one, 0.5f).From(Vector3.zero);
        }
    }
}