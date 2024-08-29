using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIManager:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private InGame _inGame;
        [SerializeField] private GameButton _button;


        private void Start()
        {
            _button.openSettings=GameObject.Find("OpenSetting").GetComponent<Button>();
            _button.openSettings.onClick.AddListener(OnOneSettingButtonClick);
            
            _button.reLoadGame.onClick.AddListener(ReloadScene);
            _button.backToGame.onClick.AddListener(OnCloseOneSettingButtonClick);
            _button.returnToMain.onClick.AddListener(ReturnMainScene);
            //TODO:调整音乐
            _button.music.onClick.AddListener(ReloadScene);
        }

        private void Update()
        {
            GlobalExit();
        }

        private void GlobalExit()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_inGame.settings.gameObject.activeSelf)
                {
                    Time.timeScale = 1f;
                    _inGame.settings.gameObject.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0f;
                    _inGame.settings.gameObject.SetActive(true);
                }
            }
        }

        public void OnStartButtonClick()
        {
            _animator.SetBool("StartGame",true);
        }
        //TODO:1.UI逻辑 2. 动画切换 3. 开始界面的设置和开发者 4.点击放苹果的逻辑 5.文字的动画 6.音乐的设置 7.局内设置的调整（重开游戏，打开设置，离开游戏）8。无目标时的移动算法

        public void OnOneSettingButtonClick()
        {
            Time.timeScale = 0;
            _inGame.settings.gameObject.SetActive(true);
        }

        public void OnCloseOneSettingButtonClick()
        {
            Time.timeScale = 1;
            _inGame.settings.gameObject.SetActive(false);
        }

        public void OnExitButtonClick()
        {
            Application.Quit();
        }

        public void ReturnMainScene()
        {
            SceneManager.LoadScene("Start");
        }

        public void EnterScreenTwo()
        {
            SceneManager.LoadScene("2");
        }

        public void EnterScreenThree()
        {
            SceneManager.LoadScene("3");
        }

        public void EnterScreenFour()
        {
            SceneManager.LoadScene("4");
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        [Serializable]
        class GameButton
        {
            [Tooltip("退出游戏")]
            public Button exitGame;
            
            [Tooltip("重启游戏")]
            public Button reLoadGame;

            [Tooltip("返回进行中的游戏，退出设置")]
            public Button backToGame;

            [Tooltip("返回主界面")]
            public Button returnToMain;

            [Tooltip("关闭音乐和音效")] 
            public Button music;

            [HideInInspector] public Button openSettings;
        }

        [Serializable]
        class  InGame
        {
            public GameObject settings;
            public GameObject deadScene;
        }
    }
}