class Course
{
    protected string _name;
    protected int _duration;

    public Course(string name, int duration)
    {
        _name = name;
        _duration = duration;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    public override string ToString()
    {
        return $"Course: {_name}, Duration: {_duration} hours";
    }
}

class OnlineCourse : Course
{
    private string _platform;

    public OnlineCourse(string name, int duration, string platform) : base(name, duration)
    {
        _platform = platform;
    }

    public string Platform
    {
        get { return _platform; }
        set { _platform = value; }
    }

    public override string ToString()
    {
        return $"OnlineCourse: {_name}, Duration: {_duration} hours, Platform: {_platform}";
    }

    public override bool Equals(object obj)
    {
        if (obj is OnlineCourse other)
        {
            return this._name == other._name && 
                   this._duration == other._duration && 
                   this._platform == other._platform;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _duration, _platform);
    }
}