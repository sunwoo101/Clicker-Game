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
        public float score;
        public float scorePerClick;
        public float scorePerSecond;
        float period;
        float time;
        float trillion = 1000000000000;
        float billion = 1000000000;
        float million = 1000000;
        [SerializeField] Text scoreText;
        [SerializeField] GameObject devButton;
        [SerializeField] EventSystem eventSystem;
        [SerializeField] GameObject clickText;
        [SerializeField] GameObject parentObject;
        #endregion

        #region Start
        private void Start()
        {
#if UNITY_EDITOR
            devButton.SetActive(true);
#endif
            period = 1f;
            score = 0;
            scorePerClick = 1;
            scorePerSecond = 0;
            LoadData();
            UpdateText();
        }
        #endregion

        #region Update
        private void Update()
        {
            ButtonFix();
            UpdateText();
            IdlePoints();
        }
        #endregion

        #region ButtonFix
        private void ButtonFix()
        {
            // Makes sure the button hover colour is the default hover colour
            eventSystem.SetSelectedGameObject(null);
        }
        #endregion

        #region LoadData
        private void LoadData()
        {
            // Load data from playerprefs
            score = PlayerPrefs.GetFloat("score", 0);
            scorePerClick = PlayerPrefs.GetFloat("scorePerClick", 1);
            scorePerSecond = PlayerPrefs.GetFloat("scorePerSecond", 1);
        }
        #endregion
        
        #region SaveData
        private void SaveData()
        {
            // Save data to playerprefs
            PlayerPrefs.SetFloat("score", score);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat("scorePerClick", scorePerClick);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat("scorePerSecond", scorePerSecond);
            PlayerPrefs.Save();
        }
        #endregion

        #region Dev
        public void Dev()
        {
            score *= 10;
        }
        #endregion

        #region ClickButton
        public void ClickButton()
        {
            score += scorePerClick;
            // Spawn in clicktext prefab
            GameObject childObject = Instantiate(clickText) as GameObject;
            //childObject.transform.parent = parentObject.transform;
            childObject.transform.SetParent(parentObject.transform, false);
        }
        #endregion

        #region IdlePoints
        private void IdlePoints()
        {
            if (Time.time > time + period)
            {
                time = Time.time;
                // Add idle points to score
                score += scorePerSecond;
            }
        }

        #endregion

        #region UpdateText
        void UpdateText()
        {
            // Display the work click if the score is equal to 0
            if (score == 0)
            {
                scoreText.text = "Click";
            }
            else
            {
                // Display a message if the score is infinity
                if (score == Mathf.Infinity)
                {
                    scoreText.text = "KM: Infinity";
                }
                // Use the word trillion if the score is above or equal to a trillion
                else if (score >= trillion)
                {
                    scoreText.text = "KM: " + (score / trillion) + " trillion";
                }
                // Use the word billion if the score is above or equal to a billion
                else if (score >= billion)
                {
                    scoreText.text = "KM: " + (score / billion) + " billion";
                }
                // Use the word million if the score is above or equal to a million
                else if (score >= million)
                {
                    scoreText.text = "KM: " + (score / million) + " million";
                }
                else
                {
                    scoreText.text = "KM: " + score;
                }
            }
            SaveData();
        }
        #endregion

        #region ResetProgress
        public void ResetProgress()
        {
            // Resets variables and deletes data from playerprefs
            PlayerPrefs.DeleteAll();
            score = 0;
            scorePerClick = 1;
            scorePerSecond = 0;
        }
        #endregion
    }
}
