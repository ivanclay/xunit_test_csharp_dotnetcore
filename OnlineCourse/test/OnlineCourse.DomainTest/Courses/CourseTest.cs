using Bogus;
using ExpectedObjects;
using OnlineCourse.DomainTest._Builders;
using OnlineCourse.DomainTest._Util;
using OnlineCouse.Domain.Courses;
using System;
using Xunit;

namespace OnlineCourse.DomainTest.Courses
{
    //Eu equanto administrador, quero criar e editar cursos para que sejam abertas matriculas para o mesmo

    //Criterio de aceite

    // - Criar um curso com nome, carga horaria, publico alvo e valor
    // - As opcoes para publico alvo devem ser: estudante, universitario, empregado e empreendedor
    // - Todos os campos do curso são obrigatorios
    // - Curso dever ter uma descricao

    public class CourseTest : IDisposable
    {
        private readonly string _name;
        private readonly double _duration;
        private readonly Audience _audience;
        private readonly double _cost;
        private readonly string _description;

        public CourseTest()
        {
            var faker = new Faker();

            _name = faker.Lorem.Word();
            _duration = faker.Random.Double(50, 720);
            _audience = Audience.Estudante;
            _cost = faker.Random.Double(100,1000);
            _description = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            //
        }

        [Fact]
        public void MustCreateCourse() 
        {
            //Arrange
            var mustCreateOnlineCourse = new
            {
                Name = _name,
                Duration = _duration,
                Audience = _audience,
                Cost = _cost,
                Description = _description
            };

            //Action
            var course = new Course(mustCreateOnlineCourse.Name, mustCreateOnlineCourse.Description, mustCreateOnlineCourse.Duration, mustCreateOnlineCourse.Audience, mustCreateOnlineCourse.Cost);

            //Assert
            mustCreateOnlineCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Fact]
        public void MustNotHaveEmptyName()
        {
            Assert
                .Throws<ArgumentException>(() => 
                new Course(string.Empty, _description, _duration, _audience, _cost))
                .WithMessage("INVALID_NAME");
        }

        [Fact]
        public void MustNotHaveNullName()
        {
            //Arrange
            var mustCreateOnlineCourse = new
            {
                Name = string.Empty,
                Duration = (double)80,
                Audience = Audience.Estudante,
                Cost = (double)950
            };

            Assert.Throws<ArgumentException>(() =>
                new Course(null, _description, _duration, _audience, _cost))
                .WithMessage("INVALID_NAME");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MustNotHaveInvalidName(string invalidName)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithName(invalidName).Build())
                .WithMessage("INVALID_NAME");
        }

        [Fact]
        public void MustNotDurationLessThanOne()
        {
            Assert.Throws<ArgumentException>(() =>
                new Course(_name, _description, 0, _audience, _cost))
                .WithMessage("INVALID_DURATION");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void MustNotHaveInvalidDuration(double invalidDuration)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithDuration(invalidDuration).Build())
                .WithMessage("INVALID_DURATION");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void MustNotCostLessThanOne(double invalidCost)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithCost(invalidCost).Build())
                .WithMessage("INVALID_COST");
        }
    }
}
