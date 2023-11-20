using System.Collections.Generic;

public class Node{
    public readonly Action Action;
    public int Cost => Action.Cost;

    public int value;
    
    public List<Node> Children = new();
    public List<Node> Parent = new();

    public Node(Action action){
        Action = action;
    }
}

public class Node2
{
    public readonly Action2 Action;
    public int Cost => Action.Cost;

    public int value;

    public List<Node2> Children = new();
    public List<Node2> Parent = new();

    public Node2(Action2 action)
    {
        Action = action;
    }
}