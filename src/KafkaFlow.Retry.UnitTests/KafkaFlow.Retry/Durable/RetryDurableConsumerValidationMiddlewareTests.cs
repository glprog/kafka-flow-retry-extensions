﻿namespace KafkaFlow.Retry.UnitTests.KafkaFlow.Retry.Durable
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using global::KafkaFlow.Retry.Durable;
    using global::KafkaFlow.Retry.Durable.Encoders;
    using global::KafkaFlow.Retry.Durable.Repository;
    using Moq;
    using Xunit;

    public class RetryDurableConsumerValidationMiddlewareTests
    {
        public static IEnumerable<object[]> DataTest()
        {
            yield return new object[]
            {
                null,
                Mock.Of<IRetryDurableQueueRepository>(),
                Mock.Of<IUtf8Encoder>()
            };
            yield return new object[]
            {
                Mock.Of<ILogHandler>(),
                null,
                Mock.Of<IUtf8Encoder>()
            };
            yield return new object[]
            {
                Mock.Of<ILogHandler>(),
                Mock.Of<IRetryDurableQueueRepository>(),
                null
            };
        }

        [Theory]
        [MemberData(nameof(DataTest))]
        internal void RetryDurableConsumerValidationMiddleware_Ctor_Tests(
            ILogHandler logHandler,
            IRetryDurableQueueRepository retryDurableQueueRepository,
            IUtf8Encoder utf8Encoder)
        {
            // Act
            Action act = () => new RetryDurableConsumerValidationMiddleware(logHandler,
                                                                            retryDurableQueueRepository,
                                                                            utf8Encoder);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}