using System.Threading.Tasks;
using JetBrains.Lifetimes;
using UnityEngine;

namespace Lailore.Lifetimes.Demo {
    public class Demo : MonoBehaviour {
        private LifetimeDefinition _lifetimeDefinition;

        private void OnEnable() {
            var gameObjectLifetime = gameObject.GetLifetime();
            _lifetimeDefinition = new LifetimeDefinition(gameObject.GetLifetime());

            LogToConsole(gameObjectLifetime);
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

        private async void LogToConsole(Lifetime lifetime) {
            while (lifetime.IsAlive) {
                Debug.Log("LogToConsole");
                await Task.Yield();
            }
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