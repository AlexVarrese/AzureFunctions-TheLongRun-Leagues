﻿using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using System;

using CQRSAzure.EventSourcing;

namespace TheLongRun.Common.Attributes
{
    /// <summary>
    /// An attribute to mark an event stream to use for output for appending events to
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class EventStreamAttribute
        : Attribute , IEventStreamUntypedIdentity
    {

        /// <summary>
        /// The domain name the aggregate instance belongs to
        /// </summary>
        private readonly string _domainName;
        public string DomainName
        {
            get
            {
                return _domainName;
            }
        }

        /// <summary>
        /// The aggregate type to which the event stream belongs
        /// </summary>
        private readonly string _aggregateTypeName;
        public string AggregateTypeName
        {
            get
            {
                return _aggregateTypeName;
            }
        }

        /// <summary>
        /// The unique identifier of the specific instance of the aggregate
        /// </summary>
        private readonly string _aggregateInstanceKey;
        [AutoResolve]
        public string InstanceKey
        {
            get
            {
                return _aggregateInstanceKey;
            }
        }

        public EventStreamAttribute(string domainName,
            string aggregateTypeName,
            string aggregateInstanceKey)
        {
            _domainName = domainName;
            _aggregateTypeName = aggregateTypeName;
            _aggregateInstanceKey = aggregateInstanceKey;
        }
    }
}
