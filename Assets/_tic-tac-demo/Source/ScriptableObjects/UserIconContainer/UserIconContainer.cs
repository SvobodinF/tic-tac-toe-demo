using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(UserIconContainer), menuName = "ScriptableObjects/UserIconContainer", order = 51)]
public class UserIconContainer : ScriptableObject
{
    [SerializeField] private Sprite _userIcon;
    [SerializeField] private Sprite _pcIcon;

    private Dictionary<Type, Sprite> @switch => new Dictionary<Type, Sprite> {
    { typeof(User), _userIcon },
    { typeof(PC), _pcIcon },
    };

    public Sprite GetSprite(Player player)
    {
        if (@switch.TryGetValue(player.GetType(), out Sprite sprite))
        {
            return sprite;
        }

        throw new NullReferenceException($"Type of {player.GetType()} hasnt sprite");
    }
}
