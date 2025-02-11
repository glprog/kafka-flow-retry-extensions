﻿namespace KafkaFlow.Retry.UnitTests.Repositories.SqlServer.Model.Schema
{
    using System;
    using FluentAssertions;
    using global::KafkaFlow.Retry.SqlServer.Model.Schema;
    using Xunit;

    public class ScriptTests
    {
        [Fact]
        public void Script_Ctor_Success()
        {
            // Arrange
            var query = "test";

            // Act
            var script = new Script(query);

            // Assert
            script.Should().NotBeNull();
            script.Value.Should().Be(query);
        }

        [Fact]
        public void Script_Ctor_WithoutValue_ThrowsException()
        {
            // Arrange
            string query = null;

            // Act
            Action act = () => new Script(query);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}