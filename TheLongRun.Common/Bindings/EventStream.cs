﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLongRun.Common.Attributes;

using CQRSAzure.EventSourcing;
using CQRSAzure.EventSourcing.Azure.Blob.Untyped;

namespace TheLongRun.Common.Bindings
{
    /// <summary>
    /// Wrapper around the CQRSAzure event stream 
    /// </summary>
    public class EventStream
    {

        private readonly IEventStreamWriterUntyped _writer = null;

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
        public string AggregateInstanceKey
        {
            get
            {
                return _aggregateInstanceKey;
            }
        }

        /// <summary>
        /// Append an event to the event stream for this aggregate instance
        /// </summary>
        /// <param name="eventToAdd">
        /// The event to append to the aggregate instance
        /// </param>
        public void AppendEvent(IEvent eventToAdd)
        {
            if (null != _writer)
            {
                _writer.AppendEvent(eventToAdd);
            }
        }

        public EventStream(string domainName,
            string aggregateTypeName,
            string aggregateInstanceKey)
            : this(new EventStreamAttribute(domainName, aggregateTypeName , aggregateInstanceKey  ) )
        {
        }

        public EventStream(EventStreamAttribute attribute)
        {
            _domainName = attribute.DomainName;
            _aggregateTypeName = attribute.AggregateTypeName;
            _aggregateInstanceKey = attribute.InstanceKey;

            // wire up the event stream writer 
            // TODO : Cater for different backing technologies... currently just AppendBlob
            _writer = new CQRSAzure.EventSourcing.Azure.Blob.Untyped.BlobEventStreamWriterUntyped(attribute); 

        }



    }
}
