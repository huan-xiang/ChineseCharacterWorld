using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cainos.Monster
{
    /// <summary>
    /// 怪物动画帧事件
    /// </summary>
    public class AnimationEventReceiver : MonoBehaviour
    {
        public UnityEvent onFootstep;
        public UnityEvent onAttackStart;
        public UnityEvent onAttackEnd;
        public UnityEvent onDieFx;
        public UnityEvent onInjureStart;
        public UnityEvent onInjureEnd;
        public void OnFootstep()
        {
            onFootstep?.Invoke();
        }

        public void OnAttackStart()
        {
            onAttackStart?.Invoke();
        }
        public void OnAttackEnd()
        {
            onAttackEnd?.Invoke();
        }

        public void OnDieFx()
        {
            onDieFx?.Invoke();
        }
        /// <summary>
        /// 受伤时
        /// </summary>
        public void OnInjureStart()
        {
            onInjureStart?.Invoke();
        }
        public void OnInjureEnd()
        {
            onInjureEnd?.Invoke();
        }
    }
}
