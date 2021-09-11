using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketClicker
{
    [AddComponentMenu("Rocket Clicker/Gameplay/UpgradeHandler")]
    public class UpgradeHandler : MonoBehaviour
    {
        [System.Serializable]
        public struct Upgrade
        {
            [Header("Variables for upgrade")]
            public string upgradeName;
            public Text buttonText;
            public Text countText;
            public float basePrice;
            [Header("This should not be changed manually")]
            public int count;
            public float price;
        }

        public Upgrade[] upgrades;
        public Upgrade test;

        #region Test
        private void Test()
        {
            if (test.count > 0)
            {
                test.price = test.basePrice * Mathf.Pow(1.15f, test.count);
            }
            else
            {
                test.price = test.basePrice;
            }
            test.price = Mathf.Round(test.price);
            test.buttonText.text = test.upgradeName + ": " + test.price + "KM";
            test.countText.text = test.upgradeName + ": " + test.count;
        }
        #endregion

        #region Update
        private void Update()
        {
            Test();
        }
        #endregion

        #region Buy
        public void Buy()
        {
            if (ScoreHandler.score >= test.price)
            {
                ScoreHandler.score -= test.price;
                test.count += 1;
            }
        }
        #endregion

        #region Sell
        public void Sell()
        {
            ScoreHandler.score += Mathf.Round(test.price * 0.6f);
            test.count -= 1;
        }
        #endregion
    }
}