using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Xunit.Gherkin.Scenarios
{
    public class ScenarioContext : IDisposable
    {
        private static ScenarioContext _current;

        private readonly ConcurrentDictionary<string, object> _data;

        public ScenarioContext()
        {
            _data = new ConcurrentDictionary<string, object>();
        }

        public static ScenarioContext Current => _current ?? (_current = new ScenarioContext());

        public void Dispose()
        {
            _current = null;
            foreach (var disposable in _data.Values.OfType<IDisposable>())
                disposable.Dispose();
        }

        public void Set<T>(string key, T value)
        {
            _data.TryAdd(key, value);
        }

        public T Get<T>(string key)
        {
            return _data.TryGetValue(key, out var value)
                ? (T) value
                : default(T);
        }

        public T GetOrAdd<T>(string key, Func<T> factory)
        {
            return (T) _data.GetOrAdd(key, k => factory());
        }
    }
}