class NodeWalker
{
    private Node _currentNode;
    private Dictionary<string, Node> _nodeDictionary;
    private char _goalChar;
    private long _steps;
    private long _lcm;

    public NodeWalker(string startingNode, char goalChar, Dictionary<string, Node> nodeDictionary)
    {
        _nodeDictionary = nodeDictionary;
        _currentNode = _nodeDictionary[startingNode];
        _goalChar = goalChar;
        
        _steps = 0;
        _lcm = 0;
    }
    
    public void Move(char direction)
    {
        _steps++;
        if (direction == 'L')
        {
            _currentNode = _nodeDictionary[_currentNode.Left];
        }
        else
        {
            _currentNode = _nodeDictionary[_currentNode.Right];
        }

        if (AtGoal)
        {
            if (_lcm == 0)
            {
                _lcm = _steps;
            }
        }
    }
    
    public Node CurrentNode => _currentNode;
    public bool AtGoal => _currentNode.Label[2] == _goalChar;
    
    public long LCM => _lcm;
    
}