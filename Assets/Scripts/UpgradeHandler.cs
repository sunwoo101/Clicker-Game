using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketClicker
{
    [AddComponentMenu("Rocket Clicker/Gameplay/UpgradeHandler")]
    public class UpgradeHandler : MonoBehaviour
    {
        #region Variables
        [System.Serializable]
        public struct Upgrade
        {
            [Header("Variables for upgrade")]
            public string upgradeName;
            public Text buyText;
            public Text sellText;
            public float basePrice;
            public Button buyButton;
            public Button sellButton;
            [Header("How many additional points per click")]
            public float scorePerClickUpgrade;
            [Header("How many additional points per second")]
            public float scorePerSecondUpgrade;
            [Header("This should not be changed")]
            public int count;
            public float price;
        }

        public Upgrade[] upgrades;
        ScoreHandler scoreHandler;
        float trillion = 1000000000000;
        float billion = 1000000000;
        float million = 1000000;
        #endregion

        #region Start
        private void Start()
        {
            scoreHandler = GetComponent<ScoreHandler>();

            LoadData();
        }
        #endregion

        #region Update
        private void Update()
        {
            DisplayPrice();
        }
        #endregion

        #region LoadData
        private void LoadData()
        {

        }
        #endregion

        #region SaveData
        private void SaveData()
        {

        }
        #endregion

        #region DisplayPrice
        private void DisplayPrice()
        {
            // Do this for every upgrade in the game
            for (int i = 0; i < upgrades.Length; i++)
            {
                // Price calculations
                #region CalculatePrice
                if (upgrades[i].count > 0)
                {
                    upgrades[i].price = upgrades[i].basePrice * Mathf.Pow(1.15f, upgrades[i].count);
                }
                else
                {
                    upgrades[i].price = upgrades[i].basePrice;
                }
                upgrades[i].price = Mathf.Round(upgrades[i].price);
                #endregion

                // Display buy price
                #region DisplayUpgradePrice
                if (upgrades[i].price == Mathf.Infinity)
                {
                    upgrades[i].buyText.text = upgrades[i].upgradeName + ": " + "Infinity KM";
                }
                // Use the word trillion if the score is above or equal to a trillion
                else if (upgrades[i].price >= trillion)
                {
                    upgrades[i].buyText.text = upgrades[i].upgradeName + ": " + (upgrades[i].price / trillion) + " trillion KM";
                }
                // Use the word billion if the score is above or equal to a billion
                else if (upgrades[i].price >= billion)
                {
                    upgrades[i].buyText.text = upgrades[i].upgradeName + ": " + (upgrades[i].price / billion) + " billion KM";
                }
                // Use the word million if the score is above or equal to a million
                else if (upgrades[i].price >= million)
                {
                    upgrades[i].buyText.text = upgrades[i].upgradeName + ": " + (upgrades[i].price / million) + " million KM";
                }
                else
                {
                    upgrades[i].buyText.text = upgrades[i].upgradeName + ": " + upgrades[i].price + "KM";
                }
                #endregion

                // Display upgrades owned
                #region DisplayUpgradesOwned
                if (upgrades[i].count == Mathf.Infinity)
                {
                    upgrades[i].sellText.text = upgrades[i].upgradeName + ": " + "Infinity owned";
                }
                // Use the word trillion if the score is above or equal to a trillion
                else if (upgrades[i].count >= trillion)
                {
                    upgrades[i].sellText.text = upgrades[i].upgradeName + ": " + (upgrades[i].count / trillion) + " owned";
                }
                // Use the word billion if the score is above or equal to a billion
                else if (upgrades[i].count >= billion)
                {
                    upgrades[i].sellText.text = upgrades[i].upgradeName + ": " + (upgrades[i].count / billion) + " owned";
                }
                // Use the word million if the score is above or equal to a million
                else if (upgrades[i].count >= million)
                {
                    upgrades[i].sellText.text = upgrades[i].upgradeName + ": " + (upgrades[i].count / million) + " owned";
                }
                else
                {
                    upgrades[i].sellText.text = upgrades[i].upgradeName + ": " + upgrades[i].count + " owned";
                }
                #endregion

                #region ChangeButtonColours
                // Change the button colour if you cant buy the upgrade
                if (scoreHandler.score >= upgrades[i].price)
                {
                    var buyButtonColor = upgrades[i].buyButton.colors;
                    buyButtonColor.normalColor = new Color(255, 255, 255, 0.3f);
                    upgrades[i].buyButton.colors = buyButtonColor;
                }
                else
                {
                    var buyButtonColor = upgrades[i].buyButton.colors;
                    buyButtonColor.normalColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
                    upgrades[i].buyButton.colors = buyButtonColor;
                }

                // Change the button colour if you cant sell the upgrade
                if (upgrades[i].count > 0)
                {
                    var sellButtonColor = upgrades[i].sellButton.colors;
                    sellButtonColor.normalColor = new Color(255, 255, 255, 0.3f);
                    upgrades[i].sellButton.colors = sellButtonColor;
                }
                else
                {
                    var sellButtonColor = upgrades[i].sellButton.colors;
                    sellButtonColor.normalColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
                    upgrades[i].sellButton.colors = sellButtonColor;
                }
                #endregion
            }
        }
        #endregion

        #region Buy
        public void Buy(int upgradeIndex)
        {
            // Buy the upgrade if you have enough money
            if (scoreHandler.score >= upgrades[upgradeIndex].price)
            {
                // Subtract from the score
                scoreHandler.score -= upgrades[upgradeIndex].price;
                // Give the upgrades
                scoreHandler.scorePerClick += upgrades[upgradeIndex].scorePerClickUpgrade;
                scoreHandler.scorePerSecond += upgrades[upgradeIndex].scorePerSecondUpgrade;
                upgrades[upgradeIndex].count += 1;
            }
        }
        #endregion

        #region Sell
        public void Sell(int upgradeIndex)
        {
            // Sell the upgrade for 60% of the price
            if (upgrades[upgradeIndex].count > 0)
            {
                // Add to the score
                scoreHandler.score += Mathf.Round(upgrades[upgradeIndex].price * 0.6f);
                // Remove the upgrades
                scoreHandler.scorePerClick -= upgrades[upgradeIndex].scorePerClickUpgrade;
                scoreHandler.scorePerSecond -= upgrades[upgradeIndex].scorePerSecondUpgrade;
                upgrades[upgradeIndex].count -= 1;
            }
        }
        #endregion

        #region ResetProgress
        public void ResetProgress()
        {
            // Resets variables and deletes data from playerprefs
            PlayerPrefs.DeleteAll();
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].count = 0;
            }
        }
        #endregion
    }
}