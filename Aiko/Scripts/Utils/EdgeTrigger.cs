using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChangeTrigger<T>
{
	public T current;
	public bool trigger;
    private static readonly Comparer<T> _comp = Comparer<T>.Default;

    public ChangeTrigger(T initial = default)
	{
		current = initial;
		trigger = false;
	}

	public bool Update(T value)
	{
        return trigger = _comp.Compare(current, current = value) != 0;
	}

    public static implicit operator T(ChangeTrigger<T> self)
    {
        return self.current;
    }

    public static implicit operator bool(ChangeTrigger<T> self)
    {
        return self.trigger;
    }
}

public class EdgeTrigger
{
    public bool current;
    public bool trigger;
    public bool rising;
    public bool falling;

    public EdgeTrigger(bool rising, bool falling)
    {
        this.rising = rising;
        this.falling = falling;
    }

    public bool Update(bool value)
    {
        return trigger = current != (current = value) && (value && rising || !value && falling);
    }

    public static implicit operator bool(EdgeTrigger self)
    {
        return self.trigger;
    }
}