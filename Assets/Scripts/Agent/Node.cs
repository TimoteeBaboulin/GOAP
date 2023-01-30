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