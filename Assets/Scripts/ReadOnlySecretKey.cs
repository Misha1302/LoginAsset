using System;
using System.Linq;

public sealed class ReadOnlySecretKey
{
    public readonly string Key;
    public readonly string User;
    public readonly bool Active;

    public ReadOnlySecretKey(string s)
    {
        var arr = s.Split("|").Select(x => x.Trim()).ToArray();
        Key = arr[0];
        User = arr[1];
        Active = DateTime.Parse(arr[2]) >= DateTime.Now;
    }

    public override string ToString() => $"{Key} | {User} | {Active}";
}