using System;
using Content.Registry;
using UnityEngine;

namespace Content.ObjectsThatExistOnScene
{
    /// <summary>
    /// base interface for the Tree of Content
    /// of prefabs that can be placed by player
    ///
    /// Examples: FlatListNavigator - everything is in a single list for demo purposes
    /// ContentTreeNavigator more complicated structure that will be implemented in the
    /// future with an enhanced navigation
    /// </summary>
    public interface IContentNavigator
    {
        void Scroll(int direction);
        //go into current selection,
        //returns false if it is a leaf - last entry of a tree
        bool TryEnter(); 
        //go to parent, returns false if it is a root root
        bool TryLeave();
        
        //where we are now
        bool IsAtLeaf { get; }
        bool IsAtRoot { get; }
        //to start from last index
        int currentIndex { get;set; }
        
        //Name that the user will see
        string CurrentDisplayName { get; }
        
        //null when not a leaf
        string CurrentPrefabID { get; }
        GameObject CurrentPrefab { get; }

        //here i return not one thing, but a tuple with several values
        //and ? is for nullable
        (string displayName, string prefabID)? GetRelative(int offset);


        //so ui and state machine knows that something changed
        event Action OnChanged;
    }
}