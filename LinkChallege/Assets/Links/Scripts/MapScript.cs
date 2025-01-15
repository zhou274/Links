using System.Collections;
using System.Collections.Generic;
using GameTemplate.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;


namespace Links.Scripts
{
    public class MapScript : MonoBehaviour, ILevelScript<Map>
    {
        [SerializeField]
        public Map level { get; set; }

        public GameObject nodePrefab;
        public GameObject emptyNodePrefab;
        public GameObject hintNodePrefab;
        public MenuManagerScript menuManager;
        public Transform nodesContainer;
        public Transform hintNodesContainer;

        public TextMeshProUGUI levelTitleText;

        public string clickid;
        private StarkAdManager starkAdManager;
        [HideInInspector]
        public int playerMoves;

        public void Awake()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var emptyNodeObject = Instantiate(emptyNodePrefab, nodesContainer);
                    var emptyNodeObjectTransform = ((RectTransform)emptyNodeObject.transform);
                    emptyNodeObjectTransform.anchorMin = new Vector2(0, 1);
                    emptyNodeObjectTransform.anchorMax = new Vector2(0, 1);
                    emptyNodeObjectTransform.anchoredPosition = new Vector2(35 + (x * 60), -35 + (-y * 60));
                    emptyNodeObjectTransform.sizeDelta = new Vector2(50, 50);
                    emptyNodeObject.name = $"empty ({x},{y})";

                    var emptyHintNodeObject = Instantiate(emptyNodePrefab, hintNodesContainer);
                    var emptyHintNodeObjectTransform = ((RectTransform)emptyHintNodeObject.transform);
                    emptyHintNodeObjectTransform.anchorMin = new Vector2(0, 1);
                    emptyHintNodeObjectTransform.anchorMax = new Vector2(0, 1);
                    emptyHintNodeObjectTransform.anchoredPosition = new Vector2(35 + (x * 60), -35 + (-y * 60));
                    emptyHintNodeObjectTransform.sizeDelta = new Vector2(50, 50);
                    emptyHintNodeObject.name = $"empty ({x},{y})";
                }
            }
        }

        public void OnEnable()
        {
            MenuManagerScript.instance.LevelStarted(level.collectionIndex, level.index);
        }

        public void LoadLevel()
        {
            playerMoves = 0;
            if (level == null)
            {
                level = new Map();
                level.GenerateRandom(5);
                level.RandomizeMap(3, new List<NodeType> { NodeType.Static, NodeType.Rotatable, NodeType.Moveable });
            }

            levelTitleText.text = level.name;

            var oldNodes = nodesContainer.GetComponentsInChildren<NodeScript>();
            foreach (var oldNode in oldNodes)
            {
                Destroy(oldNode.gameObject);
            }
            var oldHintNodes = hintNodesContainer.GetComponentsInChildren<NodeScript>();
            foreach (var oldNode in oldHintNodes)
            {
                Destroy(oldNode.gameObject);
            }

            foreach (var node in level.nodesList)
            {
                var x = node.x;
                var y = node.y;

                var nodeObject = Instantiate(nodePrefab, nodesContainer);
                nodeObject.name = $"node ({x},{y})";

                var nodeScript = nodeObject.GetComponent<NodeScript>();
                nodeScript.map = this;
                nodeScript.node = node;

                var nodeObjectTransform = ((RectTransform)nodeObject.transform);
                nodeObjectTransform.anchorMin = new Vector2(0, 1);
                nodeObjectTransform.anchorMax = new Vector2(0, 1);
                nodeObjectTransform.anchoredPosition = new Vector2(35 + (x * 60), -35 + (-y * 60));
                nodeObjectTransform.sizeDelta = new Vector2(50, 50);
            }

            foreach (var node in level.solution)
            {
                var x = node.x;
                var y = node.y;

                var nodeObject = Instantiate(nodePrefab, hintNodesContainer);
                nodeObject.name = $"node ({x},{y})";

                var nodeScript = nodeObject.GetComponent<NodeScript>();
                nodeScript.isHint = true;
                nodeScript.map = this;
                nodeScript.node = node;

                var nodeObjectTransform = ((RectTransform)nodeObject.transform);
                nodeObjectTransform.anchorMin = new Vector2(0, 1);
                nodeObjectTransform.anchorMax = new Vector2(0, 1);
                nodeObjectTransform.anchoredPosition = new Vector2(35 + (x * 60), -35 + (-y * 60));
                nodeObjectTransform.sizeDelta = new Vector2(50, 50);
            }
        }

        public bool SwitchNodes(int oldX, int oldY, int newX, int newY, NodeScript movingNode)
        {
            if (newX < 0 || newY < 0 ||
                newX >= 5 || newY >= 5 ||
                level.nodes[newX, newY] != null)
            {
                // Snap the moving node back to it's original location
                ((RectTransform)movingNode.transform).anchoredPosition = new Vector2(35 + (oldX * 60), -35 + (-oldY * 60));

                // New position is already taken
                return false;
            }

            // Update the node
            movingNode.node.x = newX;
            movingNode.node.y = newY;
            // Update the map
            level.nodes[newX, newY] = movingNode.node;
            level.nodes[oldX, oldY] = null;

            // Snap the moving node to the correct location
            ((RectTransform)movingNode.transform).anchoredPosition = new Vector2(35 + (newX * 60), -35 + (-newY * 60));

            return true;
        }

        public void RotateNode(NodeScript rotatingNode)
        {
            rotatingNode.node.Rotate();
        }

        public void CheckLevelCompleted()
        {
            if (level.CheckLevelCompleted())
            {
                var nodes = gameObject.GetComponentsInChildren<Button>();
                foreach (var node in nodes)
                {
                    node.interactable = false;
                }
                //游戏成功
                Invoke(nameof(NotifyLevelCompleted), 0.3f);
                ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
            }
        }
        public void ShowHitByDiamond()
        {
            if(PlayerPrefs.GetInt(PlayerPrefsConsts.Player.Coins)>=500)
            {
                CoinsManager.instance.AwardCoins(-500);
                if (hintNodesContainer == null)
                {
                    return;
                }

                nodesContainer.gameObject.SetActive(false);
                hintNodesContainer.gameObject.SetActive(true);

                StartCoroutine(nameof(HideHint));
            }
            else
            {
                Debug.Log("金币不足");
            }
            
            
        }
        public void ShowHint()
        {
            ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    //Debug.Log("你好");
                    if (hintNodesContainer == null)
                    {
                        return;
                    }

                    nodesContainer.gameObject.SetActive(false);
                    hintNodesContainer.gameObject.SetActive(true);

                    StartCoroutine(nameof(HideHint));


                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
            
        }

        public IEnumerator HideHint()
        {
            if (hintNodesContainer != null)
            {
                yield return new WaitForSeconds(3f);
            }

            nodesContainer.gameObject.SetActive(true);
            hintNodesContainer.gameObject.SetActive(false);
        }

        private void NotifyLevelCompleted()
        {
            var stars = playerMoves <= level.moves3stars
                ? 3
                : playerMoves <= level.moves2stars
                    ? 2
                    : 1;
            MenuManagerScript.instance.LevelCompleted(level.collectionIndex, level.index, stars);
        }



        public void getClickid()
        {
            var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
            if (launchOpt.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                    if (kv.Value != null)
                    {
                        Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                        if (kv.Key.ToString() == "clickid")
                        {
                            clickid = kv.Value.ToString();
                        }
                    }
                    else
                    {
                        Debug.Log(kv.Key + "<-参数-> " + "null ");
                    }
            }
        }

        public void apiSend(string eventname, string clickid)
        {
            TTRequest.InnerOptions options = new TTRequest.InnerOptions();
            options.Header["content-type"] = "application/json";
            options.Method = "POST";

            JsonData data1 = new JsonData();

            data1["event_type"] = eventname;
            data1["context"] = new JsonData();
            data1["context"]["ad"] = new JsonData();
            data1["context"]["ad"]["callback"] = clickid;

            Debug.Log("<-data1-> " + data1.ToJson());

            options.Data = data1.ToJson();

            TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
               response => { Debug.Log(response); },
               response => { Debug.Log(response); });
        }


        /// <summary>
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="closeCallBack"></param>
        /// <param name="errorCallBack"></param>
        public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
            }
        }

        /// <summary>
        /// 播放插屏广告
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="errorCallBack"></param>
        /// <param name="closeCallBack"></param>
        public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
                mInterstitialAd.Load();
                mInterstitialAd.Show();
            }
        }
    }
}