using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StartAnimaction : MonoBehaviour
{ 
    public void OnAnimationEnd()
    {
        SceneManager.LoadScene(1);
    }
}
