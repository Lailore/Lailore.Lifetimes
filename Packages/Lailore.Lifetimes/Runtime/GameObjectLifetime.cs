using UnityEngine;

namespace JetBrains.Lifetimes {
    public class GameObjectLifetime : MonoBehaviour {
        private LifetimeDefinition _definition;
        public Lifetime Lifetime => _definition.Lifetime;

        private void Awake() {
            _definition = new LifetimeDefinition(ApplicationLifetime.Lifetime);
        }

        private void OnDestroy() {
            _definition.Terminate();
        }
    }
}