using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tween
{
    public class TweenManager : MonoSingleton<TweenManager>
    {
        public void TestAwake()
        {
            Awake();
        }
        private readonly Dictionary<string, IEnumerator> _procedures = new Dictionary<string, IEnumerator>();

        public void StartProcedure(SKCurve curve, float time, Action<float> action, Action onComplete = null,
            string id = "")
        {
            IEnumerator enumer = StartProcedureCR(curve, time, action, onComplete, id);
            // if (!_procedures.TryAdd(id, enumer))
            // {
            //     // 处理已经存在的键
            //     Debug.LogError($"Procedure with id {id} already exists.");
            //     return;
            // }

            StartCoroutine(enumer);
        }


        private IEnumerator StartProcedureCR(SKCurve curve, float time, Action<float> action, Action onComplete = null,
            string id = "")
        {
            float deltaTime = Time.fixedDeltaTime; //0,02f
            float count = time / deltaTime; //要做循环的次数

            float step = 1 / count; //每次循环的时间间隔

            float x = 0;

            for (int i = 0; i < count; i++)
            {
                //往action里传递float（即曲线的y值）
                action(SKCurveSampler.SampleCurve(curve, x));
                x += step;
                yield return new WaitForFixedUpdate();
            }

            if (onComplete != null)
                onComplete();
        }
        public void StopProcedure(String id)
        {
            if (_procedures.TryGetValue(id, out IEnumerator enumerator))
                StopCoroutine(enumerator);
            else
                throw new InvalidOperationException("查询失败");
        }

        public void DestroyProcedure(String id)
        {
            if (_procedures.TryGetValue(id, out IEnumerator enumerator))
            {
                StopCoroutine(enumerator);
                _procedures.Remove(id);
            }
            else
                throw new InvalidOperationException("查询失败");
        }
    }
}