using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketClicker
{
    [AddComponentMenu("Rocket Clicker/Gameplay/ClickText")]
    public class ClickText : MonoBehaviour
    {
        #region Variables
        [SerializeField] Text clickText;
        [SerializeField] GameObject clickObject;
        [SerializeField] RectTransform rectTransform;
        Vector2 spawnPosition;
        float period;
        float time;
        float alpha;
        #endregion

        #region Start
        private void Start()
        {
            period = 0.1f;
            alpha = 1;
            // Set position to random
            spawnPosition.x = Random.Range(-Screen.width / 2, Screen.width / 2);
            spawnPosition.y = Random.Range(-Screen.height / 2, Screen.height / 2);
            // Set the text
            clickText.text = "Click!";
        }
        #endregion

        #region Update
        private void Update()
        {
            rectTransform.anchoredPosition = new Vector2(spawnPosition.x, spawnPosition.y);
            // Text fade
            if (Time.time > time + period)
            {
                time = Time.time;
                alpha -= 0.05f;
                clickText.CrossFadeAlpha(alpha, 0, true);
            }
            if (alpha <= 0)
            {
                Destroy(clickObject);
            }
        }
        #endregion
    }
}