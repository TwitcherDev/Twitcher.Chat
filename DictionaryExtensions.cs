namespace Twitcher.Chat;

internal static class DictionaryExtensions
{
    internal static TValue? GetTagValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        return dictionary.TryGetValue(key, out var value) ? value : default;
    }

    internal static TValue GetTagValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }

    internal static TValue GetRequiredTagValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        return dictionary.TryGetValue(key, out var value) ? value : throw new FormatException($"Tags parsing error: tag '{key}' not found");
    }

    internal static TResult? GetTagValue<TKey, TValue, TResult>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TResult defaultValue, Func<TValue, TResult> converter)
    {
        var value = GetTagValue(dictionary, key);
        if (value == null)
            return defaultValue;
        try
        {
            return converter(value);
        }
        catch (Exception e)
        {
            throw new FormatException($"Tags parsing error: tag '{key}' can't convert ({value})", e);
        }
    }

    internal static TResult GetRequiredTagValue<TKey, TValue, TResult>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, Func<TValue, TResult> converter)
    {
        var value = GetRequiredTagValue(dictionary, key);
        try
        {
            return converter(value);
        }
        catch (Exception e)
        {
            throw new FormatException($"Tags parsing error: tag '{key}' can't convert ({value})", e);
        }
    }
}
