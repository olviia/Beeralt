using System;
using UnityEngine;

namespace Attributes
{
    [Serializable]
    public class Health
    {
        [SerializeField]private int max;
        private int current;

        public int Max => max;
        public int Current => current;
        
        
        //for ui subscription
        public event Action<int> OnHealthChanged;
       

        public void Decrease(int amount)
        {
            current = Mathf.Max(current - amount, 0);
            OnHealthChanged?.Invoke(current);
        }
        public void Increase(int amount){
            current = Mathf.Min(current + amount, max);
            OnHealthChanged?.Invoke(current);
        }
    }
}