using UnityEngine;

namespace JetBrains.Lifetimes {
    public static class GameObjectLifetimeExtension {
        public static Lifetime GetLifetime(this GameObject gameObject) {
            var gameObjectLifetime = gameObject.GetComponent<GameObjectLifetime>()
                                     ?? gameObject.AddComponent<GameObjectLifetime>();
            return gameObjectLifetime.Lifetime;
        }
    }
}