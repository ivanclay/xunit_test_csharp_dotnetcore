using Bogus;
using Moq;
using OnlineCourse.DomainTest._Builders;
using OnlineCourse.DomainTest._Util;
using OnlineCouse.Domain.Courses;
using System;
using Xunit;

namespace OnlineCourse.DomainTest.Courses
{
    public class CourseStorageTest
    {
        private CourseDto _courseDto;
        private Mock<ICourseRepository> _courseRepositoryMock;
        private CourseStorage _courseStorage;
        public CourseStorageTest()
        {
            var fake = new Faker();

            _courseDto = new CourseDto
            {
                Name = fake.Random.Words(),
                Description = fake.Lorem.Paragraph(),
                Duration = fake.Random.Double(50, 1000),
                Audience = "Estudante",
                Cost = fake.Random.Double(1000, 2000)
            };

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseStorage = new CourseStorage(_courseRepositoryMock.Object);
        }

        [Fact]
        public void MustAddCourse()
        {
            _courseStorage.ToStorage(_courseDto);

            _courseRepositoryMock.Verify(r => r.ToStorage(
                It.Is<Course>(
                    c => c.Name == _courseDto.Name &&
                         c.Description == _courseDto.Description
                    )
            ));
        }

        [Fact]
        public void MustNotInformInvalidAudience()
        {
            var invalidAudience = "Medico";
            _courseDto.Audience = invalidAudience;
            Assert.Throws<ArgumentException>(() => _courseStorage.ToStorage(_courseDto));
        }

        [Fact]
        public void MustNotAddSameNameCourse()
        {
            var courseSave = CourseBuilder.New().WithName(_courseDto.Name).Build();
            _courseRepositoryMock.Setup(r => r.GetByName(_courseDto.Name)).Returns(courseSave);

            Assert
               .Throws<ArgumentException>(() =>
               _courseStorage.ToStorage(_courseDto))
               .WithMessage("EXISTS_NAME");
        }
    }

    public interface ICourseRepository 
    {
        void ToStorage(Course course);
        Course GetByName(string name);
    }
    public class CourseStorage
    {
        private ICourseRepository _courseRepository;

        public CourseStorage(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }

        public void ToStorage(CourseDto courseDto)
        {
            var courseSave = _courseRepository.GetByName(courseDto.Name);
            if (courseSave != null)
                throw new ArgumentException("EXISTS_NAME");

            if (!Enum.TryParse<Audience>(courseDto.Audience, out var audience))
                throw new ArgumentException("Invalid Audience");

            var course = new Course(courseDto.Name, courseDto.Description,courseDto.Duration, audience, courseDto.Cost);
            _courseRepository.ToStorage(course);
        }
    }

    public class CourseDto
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public double Duration { get;  set; }
        public string Audience { get;  set; }
        public double Cost { get;  set; }
    }
}