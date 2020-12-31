
using System;
using System.Collections.Concurrent;
using System.Net.Security;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ActiveMQ_MQTT_Test
{
    /// <summary>
    /// common settings
    /// </summary>
    public static class ActiveMQ_MQTT_Settings
    {
        // broker ip
        public static string s_strBrokerName = "127.0.0.1";
        // broker port
        public static int s_nBrokerPort = 61613;
        // client user name
        public static string s_strUserName = "Casun99";
        // client user password
        public static string s_strPassword = "68689168";
        // topic
        public static string s_strTopic = "ActiveMQ_MQTT_Test/StockTopic";

        public static void Trace(string strMessage)
        {
            System.Console.WriteLine(strMessage);
        }
    }

    /// <summary>
    /// producer 
    /// </summary>
    public class Producer: IDisposable
    {
        /// <summary>
        /// initial mqtt cllient and connect to ActiveMQ
        /// </summary>
        public Producer()
        {
            m_strProducerClientID = "Producer-" + s_nProducerID.ToString();
            s_nProducerID = ((0 != (s_nProducerID % int.MaxValue)) ? (++s_nProducerID) : 0);

            m_mqttProducer = new MqttClient(ActiveMQ_MQTT_Settings.s_strBrokerName,
                                            ActiveMQ_MQTT_Settings.s_nBrokerPort, 
                                            false, null, null, MqttSslProtocols.None);
            try
            {
                m_mqttProducer.Connect(m_strProducerClientID, 
                                       ActiveMQ_MQTT_Settings.s_strUserName, 
                                       ActiveMQ_MQTT_Settings.s_strPassword);
                m_bIsConnected = true;
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " Connect ActiveMQ success.");
            }
            catch (Exception ex)
            {
                ActiveMQ_MQTT_Settings.Trace("Connect ActiveMQ failed, error msg: " + ex.Message + "; error stack trace: " + ex.StackTrace);
                if (m_bIsConnected)
                {
                    m_mqttProducer.Disconnect();
                    m_bIsConnected = false;
                }                
            }
        }       

        /// <summary>
        /// disconnect producer mqtt cllient
        /// </summary>
        public void Dispose()
        {
            if (m_bIsConnected)
            {
                m_mqttProducer.Disconnect();
                m_bIsConnected = false;
            }           
        }

        /// <summary>
        /// start procuding messages
        /// </summary>
        public void StartProducing()
        {
            m_threadPublish = new Thread(PublishMessage);
            m_threadPublish.Name = m_strProducerClientID;
            m_threadPublish.IsBackground = true;
            m_threadPublish.Start();
        }

        /// <summary>
        /// publish message to the topic
        /// </summary>
        private void PublishMessage()
        {
            while (m_bIsConnected)
            {
                string strMessage = m_strProducerClientID + " produced message " + m_nMesssageSequenceNumber.ToString();
                m_nMesssageSequenceNumber = ((0 != (m_nMesssageSequenceNumber % int.MaxValue)) ? (++m_nMesssageSequenceNumber) : 0);
                m_mqttProducer.Publish(ActiveMQ_MQTT_Settings.s_strTopic,
                                       System.Text.Encoding.ASCII.GetBytes(strMessage),
                                       MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                                       false);
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " published topic content: [" + strMessage + "]");
                Thread.Sleep(100);
            }
        }
     
        // producer id
        private static int s_nProducerID = 1;
        // producer client id for mqtt
        private string m_strProducerClientID;
        // connectio state
        private bool m_bIsConnected = false;
        // publish's message sequence id
        private int m_nMesssageSequenceNumber = 1;
        // producer mqtt client
        private MqttClient m_mqttProducer;
        // thread
        private Thread m_threadPublish;
    }

    /// <summary>
    /// consume message class
    /// </summary>
    internal class ConsumeMessageInfo
    {
        public ConsumeMessageInfo(ushort nInternalMsgID, byte []arrayContent)
        {
            m_nInternalMsgID = nInternalMsgID;
            m_arrayContent = arrayContent;
        }

        public ushort InternalMsgID
        {
            get
            {
                return m_nInternalMsgID;
            }
            set
            {
                m_nInternalMsgID = value;
            }
        }

        public byte[] Content
        {
            get
            {
                return m_arrayContent;
            }
            set
            {
                m_arrayContent = value;
            }
        }


        // mqtt msg id
        private ushort m_nInternalMsgID;
        // content
        private byte[] m_arrayContent;
    }

    /// <summary>
    /// consumer
    /// </summary>
    public class Consumer : IDisposable
    {
        /// <summary>
        /// initial mqtt cllient and connect to ActiveMQ
        /// </summary>
        public Consumer()
        {
            m_strConsumerClientID = "Consumer-" + s_nConsumerID.ToString();
            s_nConsumerID = ((0 != (s_nConsumerID % int.MaxValue)) ? (++s_nConsumerID) : 0);

            m_mqttConsumer = new MqttClient(ActiveMQ_MQTT_Settings.s_strBrokerName,
                                            ActiveMQ_MQTT_Settings.s_nBrokerPort,
                                            false, null, null, MqttSslProtocols.None);
            m_mqttConsumer.MqttMsgPublishReceived += PublishReceived;
            try
            {
                m_mqttConsumer.Connect(m_strConsumerClientID,
                                       ActiveMQ_MQTT_Settings.s_strUserName,
                                       ActiveMQ_MQTT_Settings.s_strPassword);
                m_mqttConsumer.Subscribe(new string[] { ActiveMQ_MQTT_Settings.s_strTopic },
                                         new byte[]   { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                m_bIsConnected = true;
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " Connect ActiveMQ success.");
            }
            catch (Exception ex)
            {
                ActiveMQ_MQTT_Settings.Trace("Connect ActiveMQ failed, error msg: " + ex.Message + "; error stack trace: " + ex.StackTrace);
                if (m_bIsConnected)
                {
                    m_mqttConsumer.Disconnect();
                    m_bIsConnected = false;
                }                
            }
            m_queueConsumeMsg = new ConcurrentQueue<ConsumeMessageInfo>();
        }

        /// <summary>
        /// disconnect producer mqtt cllient
        /// </summary>
        public void Dispose()
        {
            if (m_bIsConnected)
            {
                m_mqttConsumer.Disconnect();
                m_bIsConnected = false;
            }
        }

        /// <summary>
        /// start procuding messages
        /// </summary>
        public void StartConsuming()
        {
            m_threadSubscribe = new Thread(ConsumeMessage);
            m_threadSubscribe.Name = m_strConsumerClientID;
            m_threadSubscribe.IsBackground = true;
            m_threadSubscribe.Start();
        }

        /// <summary>
        /// publish message to the topic
        /// </summary>
        private void ConsumeMessage()
        {
            while (m_bIsConnected)
            {
                if (!m_queueConsumeMsg.IsEmpty)
                {
                    ConsumeMessageInfo oConsumeMessageInfo;
                    if (m_queueConsumeMsg.TryDequeue(out oConsumeMessageInfo))
                    {
                        ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " received topic content: [" + 
                                                     System.Text.Encoding.ASCII.GetString(oConsumeMessageInfo.Content) + 
                                                     "] internal message id:[" + oConsumeMessageInfo.InternalMsgID + "]");
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }                
            }
        }

        /// <summary>
        /// receive message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ConsumeMessageInfo oConsumeMessageInfo = new ConsumeMessageInfo(e.MessageId, e.Message);
            m_queueConsumeMsg.Enqueue(oConsumeMessageInfo);
        }

        // producer id
        private static int s_nConsumerID = 1;
        // producer client id for mqtt
        private string m_strConsumerClientID;
        // connectio state
        private bool m_bIsConnected = false;
        // consumer mqtt client
        private MqttClient m_mqttConsumer;
        // consumer msg queue
        private ConcurrentQueue<ConsumeMessageInfo> m_queueConsumeMsg;
        // thread
        private Thread m_threadSubscribe;
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; ++i)
            {
                Consumer oConsumer = new Consumer();
                oConsumer.StartConsuming();
            }
            for (int i = 0; i < 10; ++i)
            {
                Producer oProducer = new Producer();
                oProducer.StartProducing();
            }            
            Thread.Sleep(1000 * 60 * 60 * 24);
        }
    }
}
