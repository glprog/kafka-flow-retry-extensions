﻿namespace KafkaFlow.Retry.Durable.Repository.Actions.Read
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Dawn;
    using KafkaFlow.Retry.Durable.Repository.Model;

    [ExcludeFromCodeCoverage]
    public class GetQueuesResult
    {
        public GetQueuesResult(IEnumerable<RetryQueue> retryQueues)
        {
            Guard.Argument(retryQueues, nameof(retryQueues)).NotNull();

            this.RetryQueues = retryQueues;
        }

        public IEnumerable<RetryQueue> RetryQueues { get; }
    }
}
