using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]GameObject transitionCanvas;

        void Start()
        {
            if (FindObjectsOfType<SceneController>().Length > 1)
            {
                Destroy(this);
            }
            else
                DontDestroyOnLoad(this);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadLevel(int levelId)
        {
            StartCoroutine(TransitionToScene(levelId));
        }

        public void LoadLevelNavigationScene()
        {
            StartCoroutine(TransitionToScene("LevelNavigation"));
        }

        IEnumerator TransitionToScene(string sceneName)
        {
            transitionCanvas.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneName);
            Scene myscene = SceneManager.GetSceneByName(sceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            transitionCanvas.GetComponent<Animator>().SetTrigger("SlideOut");
            yield return new WaitForSeconds(1f);
            transitionCanvas.SetActive(false);
        }

        IEnumerator TransitionToScene(int sceneBuildId)
        {
            transitionCanvas.SetActive(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneBuildId);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == sceneBuildId);
            transitionCanvas.GetComponent<Animator>().SetTrigger("SlideOut");
            yield return new WaitForSeconds(1f);
            transitionCanvas.SetActive(false);
        }

        public void LaunchFirstLevel()
        {
            StartCoroutine(TransitionToScene(1));
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
