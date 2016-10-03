1. Download Logstash from: https://www.elastic.co/downloads/logstash and extract the content
2. Go to rabbitMq Admin:
	- Create the "QueueTest2" queue 
	- Create the "app-test2" exchange 
	- GO "app-test2" exchange properties
	- Create a binding with "QueueTest2" queue
	- Use the following values:
		- To queue: "QueueTest2"
		- Routing key: "QueueTest2"

3. Copy the file LogStashRabbit.conf
4. Open command prompt as admin
5. Exec the next command: <path_logstash>\logstash agent -f <path_configfile>\LogStashRabbit.conf
6. Run the console program
7. Go to elasticseach and query the index: logstash-{yyyy.MM.dd}. Example: logstash-2016.09.30