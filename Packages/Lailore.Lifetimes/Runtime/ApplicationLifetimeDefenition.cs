using UnityEngine;

namespace JetBrains.Lifetimes {
    public static class ApplicationLifetime {
        static ApplicationLifetime() {
            if (Application.isEditor) {
                var lifetimeDefinition = new LifetimeDefinition();
                Application.quitting += () => lifetimeDefinition.Terminate();
                Lifetime = lifetimeDefinition.Lifetime;
            }
            else {
                Lifetime = Lifetime.Eternal;
            }
        }

        public static Lifetime Lifetime { get; }
    }
}