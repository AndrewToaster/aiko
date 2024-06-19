using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface INodeResult
{
    public enum ResultKind
    {
        None,
        Interact,
        Navigate
    }

    public ResultKind Kind { get; }

    public class InteractResult : INodeResult
    {
        public ResultKind Kind => ResultKind.Interact;

        public IEnumerator coroutine;

        public InteractResult(IEnumerator coroutine)
        {
            this.coroutine = coroutine;
        }
    }

    public class NavigateResult : INodeResult
    {
        public ResultKind Kind => ResultKind.Navigate;

        public IList<UINode> nodes;

        public NavigateResult(IList<UINode> nodes)
        {
            this.nodes = nodes;
        }
    }

    public class NoneResult : INodeResult
    {
        public ResultKind Kind => ResultKind.None;
    }
}
