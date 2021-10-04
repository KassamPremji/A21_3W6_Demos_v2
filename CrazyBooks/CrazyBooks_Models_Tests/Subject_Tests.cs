using CrazyBooks_Models.Models;
using NUnit.Framework;

namespace CrazyBooks_Models
{
  public class Subject_Tests
  {
    private Subject subject;
    [SetUp]
    public void Setup()
    {
      subject = new Subject();
    }
      // Tests annotations de base: pas les tests prioritaires
    [Test]
    [TestCase("")]
    [TestCase("Roman")]
    [TestCase("TAD+ Ressources humaines")]
    public void SetName_NameBetween5To25_ReturnsTrue(string subjectName)
    {
      // Arrange
      //subject = new Subject() { Name = subjectName };
      
      // Act
      //var result = new Subject() { Name = subjectName };

      // Assert
      //Assert.IsNotNull(subject.Name);
      //Assert.IsFalse(string.IsNullOrEmpty(subject.Name));
      // Démontrer erreur
      //Assert.That(() => subject.Name("", "spark"),
      //          Throws.ArgumentException);
    }

    [Test]
    [TestCase("")]
    [TestCase("TAD")]
    [TestCase("Techniques Administrative Ressources humaines")]
    public void SetName_NameInvalid_ThrowsException(string subjectName)
    {
      // Arrange
      subject = new Subject() { Name = subjectName };

      // Act Assert
      Assert.IsNotNull(subject.Name);
      Assert.IsFalse(string.IsNullOrEmpty(subject.Name));
    }
  }
}