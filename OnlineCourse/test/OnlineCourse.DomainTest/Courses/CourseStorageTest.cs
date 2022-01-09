using Moq;
using OnlineCouse.Domain.Courses;
using System;
using Xunit;

namespace OnlineCourse.DomainTest.Courses
{
    public class CourseStorageTest
    {
        [Fact]
        public void MustAddCourse()
        {
            var courseDto = new CourseDto
            {
                Name = "Name",
                Description = "Description",
                Duration = 80,
                Audience = 0,
                Cost = 100
            };

            var courseRepositoryMock = new Mock<ICourseRepository>();

            var courseStorage = new CourseStorage(courseRepositoryMock.Object);

            courseStorage.ToStorage(courseDto);

            courseRepositoryMock.Verify(r => r.ToStorage(It.IsAny<Course>()));
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
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Audience { get; set; }
        public int Cost { get; set; }
    }
}