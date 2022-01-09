using OnlineCouse.Domain.Courses;

namespace OnlineCourse.DomainTest._Builders
{
    public class CourseBuilder
    {
        private string _name = "basic computing";
        private double _duration = 80;
        private Audience _audience = Audience.Estudante;
        private double _cost = 950;
        private string _description = "A description";

        public static CourseBuilder New()
        {
            return new CourseBuilder();
        }

        public CourseBuilder WithName(string name) 
        {
            _name = name;
            return this;
        }

        public CourseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CourseBuilder WithDuration(double duration)
        {
            _duration = duration;
            return this;
        }

        public CourseBuilder WithCost(double cost)
        {
            _cost = cost;
            return this;
        }

        public CourseBuilder WithAudience(Audience audience)
        {
            _audience = audience;
            return this;
        }

        public Course Build()
        {
            return new Course(_name, _description, _duration, _audience, _cost);
        }
    }
}
