using System;
using System.Collections.Concurrent;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace MQTTNetTest
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
        //public static string s_strUserName = "Casun99";

        // client user password
        //public static string s_strPassword = "68689168";

        // topic
        public static string s_strTopic = "ActiveMQ_MQTT_Test/StockTopic";

        /// <summary>
        /// logging
        /// </summary>
        /// <param name="strMessage"></param>
        public static void Trace(string strMessage)
        {
            System.Console.WriteLine(strMessage);
        }
    }

    /// <summary>
    /// producer 
    /// </summary>
    public class Producer : IDisposable
    {
        /// <summary>
        /// initial mqtt cllient and connect to ActiveMQ
        /// </summary>
        public Producer()
        {
            m_strProducerClientID = "Producer-" + s_nProducerID.ToString();
            s_nProducerID = ((0 != (s_nProducerID % int.MaxValue)) ? (++s_nProducerID) : 0);

            m_mqttProducer = new MqttFactory().CreateMqttClient();
            m_mqttProducer.UseConnectedHandler(e => ConnectHandler(e));
            m_mqttProducer.UseDisconnectedHandler(e => DisconnectHandler(e));

            IMqttClientOptions mqttClientOptions = new MqttClientOptionsBuilder()
                                                            .WithClientId(m_strProducerClientID)
                                                            .WithTcpServer(ActiveMQ_MQTT_Settings.s_strBrokerName, ActiveMQ_MQTT_Settings.s_nBrokerPort)
                                                            .Build();
            m_mqttProducer.ConnectAsync(mqttClientOptions, CancellationToken.None);
        }

        /// <summary>
        /// disconnect producer mqtt cllient
        /// </summary>
        public void Dispose()
        {
            if (m_mqttProducer.IsConnected)
            {
                m_mqttProducer.DisconnectAsync();
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
            while (true)
            {
                if (m_mqttProducer.IsConnected)
                {
                    string strMessage = m_strProducerClientID + " produced message " + m_nMesssageSequenceNumber.ToString();
                    m_nMesssageSequenceNumber = ((0 != (m_nMesssageSequenceNumber % int.MaxValue)) ? (++m_nMesssageSequenceNumber) : 0);
                    MqttApplicationMessage mqttMessage = new MqttApplicationMessageBuilder()
                                                                .WithTopic(ActiveMQ_MQTT_Settings.s_strTopic)
                                                                .WithPayload(strMessage)
                                                                .WithExactlyOnceQoS()
                                                                .Build();
                    m_mqttProducer.PublishAsync(mqttMessage, CancellationToken.None);
                    ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + ">>> Published topic content: [" + strMessage + "]");
                    Thread.Sleep(100);
                }
                else
                {
                    Thread.Sleep(1000);
                }
                
            }
        }

        /// <summary>
        /// reconnect
        /// </summary>
        private void Reconnect()
        {
            if (!m_mqttProducer.IsConnected)
            {
                IMqttClientOptions mqttClientOptions = new MqttClientOptionsBuilder()
                                                            .WithClientId(m_strProducerClientID)
                                                            .WithTcpServer(ActiveMQ_MQTT_Settings.s_strBrokerName, ActiveMQ_MQTT_Settings.s_nBrokerPort)
                                                            .Build();
                m_mqttProducer.ConnectAsync(mqttClientOptions, CancellationToken.None);
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " Reconnecting.");
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// connect event handler
        /// </summary>
        /// <param name="e"></param>
        private void ConnectHandler(MqttClientConnectedEventArgs e)
        {
            if (MqttClientConnectResultCode.Success == e.AuthenticateResult.ResultCode)
            {
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " Connect ActiveMQ success.");
            }
            else
            {
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + "Connect ActiveMQ failed, error code: " + e.AuthenticateResult.ResultCode.ToString());
                Reconnect();
            }
        }

        /// <summary>
        /// disconnect event handler
        /// </summary>
        /// <param name="e"></param>
        private void DisconnectHandler(MqttClientDisconnectedEventArgs e)
        {
            if (MqttClientDisconnectReason.NormalDisconnection == e.ReasonCode)
            {
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " disconnect ActiveMQ normally.");
            }
            else
            {
                ActiveMQ_MQTT_Settings.Trace(m_strProducerClientID + " disconnect ActiveMQ with error code: " + e.ReasonCode.ToString());                
            }
            Reconnect();            
        }

        // producer id
        private static int s_nProducerID = 1;

        // producer client id for mqtt
        private string m_strProducerClientID;

        // publish's message sequence id
        private int m_nMesssageSequenceNumber = 1;

        // producer mqtt client
        private IMqttClient m_mqttProducer;

        // thread
        private Thread m_threadPublish;
    }

    /// <summary>
    /// consume message class
    /// </summary>
    internal class ConsumeMessageInfo
    {
        public ConsumeMessageInfo(byte[] arrayContent)
        {
            m_arrayContent = arrayContent;
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

            m_mqttConsumer = new MqttFactory().CreateMqttClient();
            m_mqttConsumer.UseApplicationMessageReceivedHandler(e => PublishReceived(e));
            m_mqttConsumer.UseConnectedHandler(e => ConnectHandler(e));
            m_mqttConsumer.UseDisconnectedHandler(e => DisconnectHandler(e));

            IMqttClientOptions mqttClientOptions = new MqttClientOptionsBuilder()
                                                        .WithClientId(m_strConsumerClientID)
                                                        .WithTcpServer(ActiveMQ_MQTT_Settings.s_strBrokerName, ActiveMQ_MQTT_Settings.s_nBrokerPort)
                                                        .Build();
            m_mqttConsumer.ConnectAsync(mqttClientOptions, CancellationToken.None);

            m_queueConsumeMsg = new ConcurrentQueue<ConsumeMessageInfo>();
        }

        /// <summary>
        /// disconnect producer mqtt cllient
        /// </summary>
        public void Dispose()
        {
            if (m_mqttConsumer.IsConnected)
            {
                m_mqttConsumer.DisconnectAsync();
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
            while (true)
            {
                if (m_mqttConsumer.IsConnected && (!m_queueConsumeMsg.IsEmpty))
                {
                    ConsumeMessageInfo oConsumeMessageInfo;
                    if (m_queueConsumeMsg.TryDequeue(out oConsumeMessageInfo))
                    {
                        ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + "<<< Received topic content: [" +
                                                     System.Text.Encoding.ASCII.GetString(oConsumeMessageInfo.Content) +
                                                     "]");
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// reconnect
        /// </summary>
        private void Reconnect()
        {
            if (!m_mqttConsumer.IsConnected)
            {
                IMqttClientOptions mqttClientOptions = new MqttClientOptionsBuilder()
                                                            .WithClientId(m_strConsumerClientID)
                                                            .WithTcpServer(ActiveMQ_MQTT_Settings.s_strBrokerName, ActiveMQ_MQTT_Settings.s_nBrokerPort)
                                                            .Build();
                m_mqttConsumer.ConnectAsync(mqttClientOptions, CancellationToken.None);
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " Reconnecting.");
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// connect event handler
        /// </summary>
        /// <param name="e"></param>
        private void ConnectHandler(MqttClientConnectedEventArgs e)
        {
            if (MqttClientConnectResultCode.Success == e.AuthenticateResult.ResultCode)
            {
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " Connect ActiveMQ success.");
                MqttTopicFilter mqttTopicFilter = new MqttTopicFilterBuilder()
                                        .WithTopic(ActiveMQ_MQTT_Settings.s_strTopic)
                                        .WithExactlyOnceQoS()
                                        .Build();
                m_mqttConsumer.SubscribeAsync(mqttTopicFilter);
            }
            else
            {
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + "Connect ActiveMQ failed, error code: " + e.AuthenticateResult.ResultCode.ToString());
                Reconnect();
            }
        }

        /// <summary>
        /// disconnect event handler
        /// </summary>
        /// <param name="e"></param>
        private void DisconnectHandler(MqttClientDisconnectedEventArgs e)
        {
            if (MqttClientDisconnectReason.NormalDisconnection == e.ReasonCode)
            {
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " disconnect ActiveMQ normally.");
            }
            else
            {
                ActiveMQ_MQTT_Settings.Trace(m_strConsumerClientID + " disconnect ActiveMQ with error code: " + e.ReasonCode.ToString());                
            }
            Reconnect();
        }

        /// <summary>
        /// receive message
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private void PublishReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            ConsumeMessageInfo oConsumeMessageInfo = new ConsumeMessageInfo(e.ApplicationMessage.Payload);
            m_queueConsumeMsg.Enqueue(oConsumeMessageInfo);
        }

        // producer id
        private static int s_nConsumerID = 1;

        // producer client id for mqtt
        private string m_strConsumerClientID;

        // consumer mqtt client
        private IMqttClient m_mqttConsumer;

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
