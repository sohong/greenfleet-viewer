////////////////////////////////////////////////////////////////////////////////
// TopicPublisher.cs
// 2012.03.06, Deleted by sohong
//
// =============================================================================
// Copyright (C) 2011 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

namespace GreenFleet.Messaging {

    /// <summary>
    /// ActiveMQ topic producer.
    /// </summary>
    public class TopicPublisher : TopicClient {

        #region fields
        #endregion // fields


        #region constructors

        public TopicPublisher() {
        }

        #endregion // constructors


        #region overriden methods

        protected override IDisposable CreateClient(ISession session, ITopic topic) {
            return session.CreateProducer(topic);
        }

        #endregion // overriden methods
    }
}
