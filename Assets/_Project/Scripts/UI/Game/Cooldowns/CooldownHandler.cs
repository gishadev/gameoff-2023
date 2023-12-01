﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace gameoff.UI.Game
{
    public class CooldownHandler : MonoBehaviour
    {
        [SerializeField] private Image cooldownImage;

        private CancellationTokenSource _cts;

        protected virtual void Awake()
        {
            _cts = new CancellationTokenSource();
            _cts.RegisterRaiseCancelOnDestroy(gameObject);

            cooldownImage.fillAmount = 0f;
        }

        public async void ShowCooldown(float cooldownTime)
        {
            if (_cts.IsCancellationRequested)
                return;

            cooldownImage.fillAmount = 1f;
            while (cooldownImage.fillAmount > 0f)
            {
                await UniTask.Yield(cancellationToken: _cts.Token).SuppressCancellationThrow();

                if (_cts.IsCancellationRequested)
                    return;

                cooldownImage.fillAmount -= Time.deltaTime / cooldownTime;
            }

            cooldownImage.fillAmount = 0f;
        }
    }
}