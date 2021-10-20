using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TechCase.Application.Common.Exception;
using Xunit;

namespace TechCase.Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void DefaultConstructor_EmptyError_CreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Fact]
        public void SingleValidationFailure_SingeElement_CreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("UserId", "UserId cannot be empty"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "UserId" });
            actual["UserId"].Should().BeEquivalentTo(new string[] { "UserId cannot be empty" });
        }

        [Fact]
        public void MulitpleValidationFailure_Multiple_CreatesAMultipleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("UserId", "UserId cannot be empty"),
                new ValidationFailure("CartItems", "CartItems cannot be null")
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "UserId", "CartItems" });

            actual["UserId"].Should().BeEquivalentTo(new string[]
            {
                "UserId cannot be empty"
            });

            actual["CartItems"].Should().BeEquivalentTo(new string[]
            {
                "CartItems cannot be null"
            });
        }
    }
}