using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReactiveProperty<T>
{
    [SerializeField] private T _value = default;

    public event Action<T> OnChangedWithValue;
    public event Action OnChanged;

    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(this._value, value))
            {
                this._value = value;
                OnChangedWithValue?.Invoke(this._value);
                OnChanged?.Invoke();
            }
        }
    }

    public ReactiveProperty(T initialValue = default)
    {
        _value = initialValue;
    }

    public void Subscribe(Action<T> callback)
    {
        OnChangedWithValue += callback;
        callback?.Invoke(_value);
    }

    public void Subscribe(Action callback)
    {
        OnChanged += callback;
        callback?.Invoke();
    }

    public void Unsubscribe(Action<T> callback)
    {
        OnChangedWithValue -= callback;
    }

    public void Unsubscribe(Action callback)
    {
        OnChanged -= callback;
    }

    public void Notify()
    {
        OnChangedWithValue?.Invoke(_value);
        OnChanged?.Invoke();
    }
    public void SetValueWithoutNotify(T value) =>  _value = value;
    public void SetValue(T value) =>  Value = value;

    // Неявное преобразование ReactiveProperty<T> в T
    public static implicit operator T(ReactiveProperty<T> reactiveProperty)
    {
        return reactiveProperty._value;
    }
}
