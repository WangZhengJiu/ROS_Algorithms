## activemq.xml
```
<plugins>    
  <loggingBrokerPlugin logAll="true" logProducerEvents="true" logConsumerEvents="true"/>           
</plugins>
```

## log4j.properties
```
log4j.logger.org.apache.activemq.transport.tcp=TRACE
log4j.logger.org.apache.activemq.transport.failover.FailoverTransport=DEBUG
log4j.logger.org.apache.activemq.transport.TransportLogger=DEBUG
log4j.logger.org.apache.activemq.broker.TransportConnection=DEBUG
log4j.logger.org.apache.activemq=DEBUG
```
