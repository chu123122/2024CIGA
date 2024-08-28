using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void OnStartButtonDown()
        {
            _animator.SetBool("StartGame",true);
        }
        //TODO:1.UI逻辑 2. 动画切换 3. 开始界面的设置和开发者 4.点击放苹果的逻辑 5.文字的动画 6.音乐的设置 7.局内设置的调整（重开游戏，打开设置，离开游戏）
        public void EnterScreenTwo(){}
        public void EnterScreenThree(){}
        public void EnterScreenFour(){}
        
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}