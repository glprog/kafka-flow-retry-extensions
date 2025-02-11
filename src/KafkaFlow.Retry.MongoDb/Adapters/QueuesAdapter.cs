﻿namespace KafkaFlow.Retry.MongoDb.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dawn;
    using KafkaFlow.Retry.Durable.Repository.Model;
    using KafkaFlow.Retry.MongoDb.Adapters.Interfaces;
    using KafkaFlow.Retry.MongoDb.Model;

    internal class QueuesAdapter : IQueuesAdapter
    {
        private readonly IItemAdapter itemAdapter;

        public QueuesAdapter(IItemAdapter itemAdapter)
        {
            Guard.Argument(itemAdapter, nameof(itemAdapter)).NotNull();

            this.itemAdapter = itemAdapter;
        }

        public IEnumerable<RetryQueue> Adapt(IEnumerable<RetryQueueDbo> queuesDbo, IEnumerable<RetryQueueItemDbo> itemsDbo)
        {
            var queuesDictionary = new Dictionary<Guid, RetryQueue>
            (
                queuesDbo.ToDictionary
                (
                    queueDbo => queueDbo.Id,
                    queueDbo => this.Adapt(queueDbo)
                )
            );

            foreach (var itemDbo in itemsDbo)
            {
                Guard.Argument(queuesDictionary.ContainsKey(itemDbo.RetryQueueId), nameof(itemDbo.RetryQueueId))
                     .True($"{nameof(itemDbo.RetryQueueId)} not found in queues list.");

                queuesDictionary[itemDbo.RetryQueueId].AddItem(this.itemAdapter.Adapt(itemDbo));
            }

            return queuesDictionary.Values;
        }

        private RetryQueue Adapt(RetryQueueDbo queueDbo)
        {
            return new RetryQueue(
                queueDbo.Id,
                queueDbo.SearchGroupKey,
                queueDbo.QueueGroupKey,
                queueDbo.CreationDate,
                queueDbo.LastExecution,
                queueDbo.Status);
        }
    }
}