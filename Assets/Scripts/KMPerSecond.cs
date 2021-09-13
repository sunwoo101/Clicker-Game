using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketClicker
{
    [AddComponentMenu("Rocket Clicker/Gameplay/KMPerSecond")]
    public class KMPerSecond : MonoBehaviour
    {
        #region Variables
        float period;
        float time;
        float clicks;
        ScoreHandler scoreHandler;
        [SerializeField] Text clicksPerSecondText;
        #endregion

        #region Start
        private void Start()
        {
            period = 1f;
            scoreHandler = GetComponent<ScoreHandler>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Time.time > time + period)
            {
                time = Time.time;
                // Display KM Per Second
                clicksPerSecondText.text = "KM Per Second: " + (clicks + scoreHandler.scorePerSecond);
                clicks = 0;
            }
        }
        #endregion

        #region Click
        public void Click()
        {
            // Count clicks
            clicks += scoreHandler.scorePerClick;
        }
        #endregion
    }
}