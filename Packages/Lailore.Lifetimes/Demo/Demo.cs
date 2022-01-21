using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Lifetimes;
using UnityEngine;

namespace Lailore.Lifetimes.Demo {
    public class Demo : MonoBehaviour {
        private LifetimeDefinition _lifetimeDefinition;

        private void OnEnable() {
            _lifetimeDefinition = new LifetimeDefinition();
            StartHardWork(_lifetimeDefinition.Lifetime);
        }

        private void OnDisable() {
            _lifetimeDefinition.Terminate();
        }

        private async void StartHardWork(Lifetime lifetime) {
            lifetime.Bracket(() => { }, () => { });
            
            Rotation(lifetime);
            Move(lifetime);
        }

        private async void Rotation(Lifetime lifetime) {
            var cachedTransform = transform;
            while (lifetime.IsAlive) {
                cachedTransform.Rotate(Vector3.up, 90 * Time.deltaTime);
                await Task.Yield();
            }
        }

        private async void Move(Lifetime lifetime) {
            var cachedTransform = transform;
            while (lifetime.IsAlive) {
                var position = cachedTransform.position;
                cachedTransform.position = Vector3.MoveTowards(
                    position,
                    new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time), 0),
                    5 * Time.deltaTime);
                await Task.Yield();
            }   
        }
    }
}