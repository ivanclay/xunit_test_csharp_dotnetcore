using Bogus;
using Moq;
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
                Audience = 1,
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
    }

    public interface ICourseRepository 
    {
        void ToStorage(Course course);
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
            var course = new Course(courseDto.Name, courseDto.Description,courseDto.Duration, Audience.Estudante, courseDto.Cost);
            _courseRepository.ToStorage(course);
        }
    }

    public class CourseDto
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public double Duration { get;  set; }
        public int Audience { get;  set; }
        public double Cost { get;  set; }
    }
}