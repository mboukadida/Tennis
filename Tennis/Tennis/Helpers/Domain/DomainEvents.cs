using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Events;

namespace Tennis.Helpers.Domain
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;

        public static IWindsorContainer Container { get; set; }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container != null)
            {
                foreach (var handler in Container.ResolveAll<IHandler<T>>())
                    handler.Handle(args);
                //var t = Container.Resolve<IGameStartedHandler>();
                //t.Handle(args as GameStarted);
            }

            if (actions != null)
            { 
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
            }
        }
    }
}
