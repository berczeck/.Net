Create one queue
Create one exchange
Bind queue and exchange with the same name:
	TO queue: queuname
	Routing key: queuename

TO execute logstash  go to bin directory and execute
 logstash agent -f "PATH"