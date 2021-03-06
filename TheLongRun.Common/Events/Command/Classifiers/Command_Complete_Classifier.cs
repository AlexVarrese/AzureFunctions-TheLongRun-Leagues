﻿using System;
using System.Collections;
using System.Collections.Generic;

using CQRSAzure.EventSourcing;
using CQRSAzure.EventSourcing.IdentityGroups;
using CQRSAzure.IdentifierGroup;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;
using TheLongRun.Common.Events.Command;

namespace TheLongRun.Common.Events.Command.Classifiers
{
    /// <summary>
    /// A classifier to determine whether a command is complete or not
    /// </summary>
    [CQRSAzure.EventSourcing.DomainNameAttribute("Command")]
    [CQRSAzure.EventSourcing.Category("Command")]
    public sealed class Command_Complete_Classifier
        : IClassifierUntyped,
        IClassifierEventHandler<ProcessingCompleted >
    {

        /// <summary>
        /// This classifier does not support snapshots as the number of events in a command is typically too small
        /// to make that worthwhile
        /// </summary>
        public bool SupportsSnapshots => false  ;

        /// <summary>
        /// This classifier gets its setting from reading the event stream 
        /// </summary>
        public IClassifier.ClassifierDataSourceType ClassifierDataSource => IClassifier.ClassifierDataSourceType.EventHandler;

        /// <summary>
        /// If we encounter a "ProcessingCompleted" event then this command is in the completed set
        /// </summary>
        /// <param name="eventClassName">
        /// Name of the event being handled
        /// </param>
        /// <param name="eventToHandle">
        /// Event data of the event being handled
        /// </param>
        public IClassifierDataSourceHandler.EvaluationResult EvaluateEvent(string eventClassName, 
            JObject eventToHandle)
        {
            if (eventClassName == typeof(ProcessingCompleted).Name)
            {
                return EvaluateEvent(eventToHandle.ToObject<ProcessingCompleted>());
            }

            return IClassifierDataSourceHandler.EvaluationResult.Unchanged;
        }

        public IClassifierDataSourceHandler.EvaluationResult EvaluateEvent(ProcessingCompleted eventToEvaluate)
        {
            return  IClassifierDataSourceHandler.EvaluationResult.Include;
        }

        /// <summary>
        /// This classifier does not get its data from a projection 
        /// </summary>
        public IClassifierDataSourceHandler.EvaluationResult EvaluateProjection<TProjection>(TProjection projection) where TProjection : IProjectionUntyped
        {
            throw new NotImplementedException();
        }

        public bool HandlesEventType(string eventTypeName)
        {
            if (eventTypeName == typeof(ProcessingCompleted).Name )
            {
                return true;
            }
            return false;
        }

        #region "Snapshot"
        public void LoadFromSnapshot(IClassifierSnapshotUntyped latestSnapshot)
        {
            throw new NotImplementedException();
        }

        public IClassifierSnapshotUntyped ToSnapshot()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
