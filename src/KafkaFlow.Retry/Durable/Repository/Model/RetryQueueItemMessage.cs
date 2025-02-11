﻿namespace KafkaFlow.Retry.Durable.Repository.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Dawn;

    [ExcludeFromCodeCoverage]
    public class RetryQueueItemMessage
    {
        public RetryQueueItemMessage(
            string topicName,
            byte[] key,
            byte[] value,
            int partition,
            long offset,
            DateTime utcTimeStamp,
            IEnumerable<MessageHeader> headers = null
            )
        {
            Guard.Argument(topicName).NotNull().NotEmpty();
            Guard.Argument(value).NotNull().NotEmpty();
            Guard.Argument(partition).NotNegative();
            Guard.Argument(offset).NotNegative();
            Guard.Argument(utcTimeStamp).NotDefault();

            this.TopicName = topicName;
            this.Key = key;
            this.Value = value;
            this.Partition = partition;
            this.Offset = offset;
            this.UtcTimeStamp = utcTimeStamp;
            this.Headers = headers?.ToList() ?? new List<MessageHeader>();
        }

        public IList<MessageHeader> Headers { get; }

        public byte[] Key { get; }

        public long Offset { get; }

        public int Partition { get; }

        public string TopicName { get; }

        public DateTime UtcTimeStamp { get; }

        public byte[] Value { get; }

        public void AddHeader(MessageHeader header)
        {
            this.Headers.Add(header);
        }
    }
}
