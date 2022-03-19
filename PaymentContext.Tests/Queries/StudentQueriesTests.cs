using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries
{
    // Red, Green, Refactory     
    // ShouldReturnErrorWhen
    // ShouldReturnSuccessWhen
    // ShouldReturnNullWhen

    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            for (var i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "@gmail.com"))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenStudentDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentDocumentInfo("12345678999");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnNullWhenStudentDocumentExists()
        {
            var exp = StudentQueries.GetStudentDocumentInfo("11111111111");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreNotEqual(null, student);
        }

        public void ShouldReturnNullWhenStudentEmailNotExists()
        {
            var exp = StudentQueries.GetStudentEmailInfo("ivan@gmail.com");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnNullWhenStudentEmailExists()
        {
            var exp = StudentQueries.GetStudentEmailInfo("1@gmail.com");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreNotEqual(null, student);
        }

    }

}