using System;

namespace OnlineCouse.Domain.Courses
{
    public class Course
    {
        public Course(string name, string description, double duration, Audience audience, double cost)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("INVALID_NAME");
            if (duration < 1)
                throw new ArgumentException("INVALID_DURATION");
            if (cost < 1)
                throw new ArgumentException("INVALID_COST");

            Name = name;
            Duration = duration;
            Audience = audience;
            Cost = cost;
            Description = description;
        }

        public string Name { get; private set; }
        public double Duration { get; private set; }
        public Audience Audience { get; private set; }
        public double Cost { get; private set; }
        public string Description { get; private set; }
    }
}
