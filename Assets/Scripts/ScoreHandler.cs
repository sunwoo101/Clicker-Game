using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RocketClicker
{
    [AddComponentMenu("Rocket Clicker/Gameplay/ScoreHandler")]
    public class ScoreHandler : MonoBehaviour
    {
        #region Variables
        public static float score;
        public static float scorePerClick;
        public static float scorePerSecond;
        float trillion = 1000000000000;
        float billion = 1000000000;
        float million = 1000000;
        [SerializeField] Text scoreText;
        [SerializeField] GameObject devButton;
        [SerializeField] EventSystem eventSystem;
        #endregion

        #region Start
        private void Start()
        {
#if UNITY_EDITOR
            devButton.SetActive(true);
#endif
            Load();
            UpdateText();
        }
        #endregion

        #region Update
        private void Update()
        {
            ButtonFix();
        }
        #endregion

        #region ButtonFix
        private void ButtonFix()
        {
            eventSystem.SetSelectedGameObject(null);
        }
        #endregion

        #region Load
        private void Load()
        {
            score = PlayerPrefs.GetFloat("score", 0);
            scorePerClick = PlayerPrefs.GetFloat("scorePerClick", 1);
            scorePerSecond = PlayerPrefs.GetFloat("scorePerSecond", 0);
        }
        #endregion

        #region Dev
        public void Dev()
        {
            score *= 10;
            UpdateText();
        }
        #endregion

        #region ClickButton
        public void ClickButton()
        {
            score += scorePerClick;
            UpdateText();
        }
        #endregion

        #region UpdateText
        void UpdateText()
        {
            // If score is 0
            if (score == 0)
            {
                scoreText.text = "Click"; // Show click text
            }
            else
            {
                // If score is infinity
                if (score == Mathf.Infinity)
                {
                    scoreText.text = "You have reached the end"; // Display message for infinite score
                }
                // If score is over a trillion
                else if (score >= trillion)
                {
                    scoreText.text = "KM: " + (score / trillion) + " trillion"; // Display score with the trillion text
                }
                // If score is over a billion
                else if (score >= billion)
                {
                    scoreText.text = "KM: " + (score / billion) + " billion"; // Display score with the billion text
                }
                // If score is over a million
                else if (score >= million)
                {
                    scoreText.text = "KM: " + (score / million) + " million"; // Display score with the million text
                }
                else
                {
                    scoreText.text = "KM: " + score; // Display score
                }
            }

            Debug.Log("Score: " + score);
            Debug.Log("Score Per Click: " + scorePerClick);
            Debug.Log("Score Per Second: " + scorePerSecond);

            PlayerPrefs.SetFloat("score", score);
            PlayerPrefs.SetFloat("scorePerClick", scorePerClick);
            PlayerPrefs.SetFloat("scorePerClick", scorePerSecond);
            PlayerPrefs.Save();
        }
        #endregion

        #region ResetProgress
        public void ResetProgress()
        {
            PlayerPrefs.DeleteAll();
            score = 0;
            scorePerClick = 1;
            scorePerSecond = 0;
            UpdateText();
        }
        #endregion
    }
}
