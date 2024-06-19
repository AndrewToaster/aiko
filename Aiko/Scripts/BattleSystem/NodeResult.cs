using System.Collections;
using System.Collections.Generic;
using static INodeResult;

public static class NodeResult
{
    public static INodeResult Interact(IEnumerator coroutine) => new InteractResult(coroutine);
    public static INodeResult Navigate(IList<UINode> nodes) => new NavigateResult(nodes);
    public static readonly INodeResult None = new NoneResult();
}