using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lucyana.EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> bindings =
            new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);

        public static void Unregister(EventBinding<T> binding) => bindings.Remove(binding);

        public static void Invoke(T @event = default)
        {
            HashSet<IEventBinding<T>> snapShot = new HashSet<IEventBinding<T>>(bindings);
            foreach (IEventBinding<T> binding in snapShot)
            {
                if (!bindings.Contains(binding))
                    continue;

                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        public static void Clear()
        {
            bindings.Clear();
        }

        [MustUseReturnValue]
        public static EventBinding<T> CreateAndRegister([System.Diagnostics.CodeAnalysis.NotNull] Action action)
        {
            EventBinding<T> binding = new EventBinding<T>(action);
            Register(binding);
            return binding;
        }

        [MustUseReturnValue]
        public static EventBinding<T> CreateAndRegister([System.Diagnostics.CodeAnalysis.NotNull] Action<T> action)
        {
            EventBinding<T> binding = new EventBinding<T>(action);
            Register(binding);
            return binding;
        }
    }
}