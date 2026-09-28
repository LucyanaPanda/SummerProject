using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lucyana.EventBus
{
    public class LocalEventBus<T>
        where T : IEvent
    {
        private readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public void Register(IEventBinding<T> binding) => bindings.Add(binding);

        public void Unregister(IEventBinding<T> binding) => bindings.Remove(binding);

        public void Invoke(T @event = default)
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

        public void Clear() => bindings.Clear();

        [MustUseReturnValue]
        public EventBinding<T> CreateAndRegister([System.Diagnostics.CodeAnalysis.NotNull] Action d)
        {
            EventBinding<T> binding = new EventBinding<T>(d);
            Register(binding);
            return binding;
        }

        [MustUseReturnValue]
        public EventBinding<T> CreateAndRegister(
            [System.Diagnostics.CodeAnalysis.NotNull] Action<T> d
        )
        {
            EventBinding<T> binding = new EventBinding<T>(d);
            Register(binding);
            return binding;
        }
    }
}