using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GameTemplate.Scripts
{
    public class CoinsManager : MonoBehaviour
    {
        public UnityEvent OnCoinsAwarded = new UnityEvent();
        
        public static CoinsManager instance;
        
        [HideInInspector]
        public int coins;

        public int coinsToAwardForLevelCompletion = 100;

        [Header("Spawning")]
        public GameObject coinPrefab;
        public Transform spawnPositionCenter;
        public int spawnRadius;

        public Transform destinationPosition;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
            coins = PlayerPrefs.GetInt(PlayerPrefsConsts.Player.Coins, 0);
        }

        public void AwardCoins(int coinsToAward)
        {
            var newCoins = coins + coinsToAward;

            PlayerPrefs.SetInt(PlayerPrefsConsts.Player.Coins, newCoins);

            coins = newCoins;

            if (coinPrefab != null)
            {
                var coinsToSpawn = Math.Min(coinsToAward, 20);
                
                for (int i = 0; i < coinsToSpawn; i++)
                {
                    var coin = Instantiate(coinPrefab, spawnPositionCenter);
                    var delta = Random.insideUnitCircle * spawnRadius;
                    coin.transform.position = new Vector3(spawnPositionCenter.position.x + delta.x, spawnPositionCenter.position.y + delta.y, spawnPositionCenter.position.z);

                    var sequence = DOTween.Sequence();
                    sequence.Append(coin.transform.DOMove(destinationPosition.position, 1.5f).SetEase(Ease.OutCubic));
                    sequence.OnComplete(() => DestroyImmediate(coin));
                }
            }

            if (OnCoinsAwarded != null)
            {
                OnCoinsAwarded.Invoke();
            }
        }

        public void AwardCoinsForLevelCompleted()
        {
            AwardCoins(coinsToAwardForLevelCompletion);
        }
    }
}