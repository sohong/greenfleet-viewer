////////////////////////////////////////////////////////////////////////////////
// TopicClient.cs
// 2012.03.06, Deleted by sohong
//
// =============================================================================
// Copyright (C) 2011 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

namespace GreenFleet.Messaging {

    /// <summary>
    /// ActiveMQ client base.
    /// </summary>
    public abstract class TopicClient {

        #region fields

        private IConnection m_connection;
        private ISession m_session;
        private ITopic m_topic;
        private IDisposable m_client;

        #endregion // fields


        #region constructors
        #endregion constructors


        #region methods

        public void Connect(string clientId, string topicName, string host, int ip = 61616) {
            string broker = "tcp://" + host + ":" + ip;

            m_connection = new ConnectionFactory(broker, clientId).CreateConnection();
            m_session = m_connection.CreateSession();
            m_topic = new ActiveMQTopic(topicName);
            //m_clinet = m_session.CreateProducer(m_topic);
            m_client = CreateClient(m_session, m_topic);
        }

        public void Dispose() {
            if (m_client != null) {
                m_client.Dispose();
                m_client = null;

                m_session.Close();
                m_session.Dispose();
                m_session = null;

                m_connection.Stop();
                m_connection.Close();
                m_connection.Dispose();
                m_connection = null;
            }
        }

        #endregion // methods


        #region internal methods

        protected abstract IDisposable CreateClient(ISession session, ITopic topic);
        
        #endregion // internal methods

    }
}
