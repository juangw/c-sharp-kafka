How to Add a New Topic Consumer & Producer

1. Add new `avsc` file under `c_sharp_kafka/Schemas`. Utilize https://toolslick.com/generation/metadata/avro-schema-from-json to convert known JSON schema to avro file
2. Start all kafka docker containers running with `docker-compose up -d`. Confirm they are up with `docker-compose ps`
3. Generate `cs` files from `avsc` files using this commmand `avrogen -s c_sharp_kafka/Schemas/<new_avsc_schema_file>.avsc .`
4. Create new consumer and producer classes for your topic under the `c_sharp_kafka/Consumers` & `c_sharp_kafka/Producers` folders respectively using the base classes.
5. Add your new topic to `Program.cs` in the case statements to handle them from program arguments
6. Test new topic production and consumption by changing your args passed into program

NOTE: If developing locally, you may have to remove any previous iterations of `avsc` schemas by running this command `curl -X DELETE http://localhost:8081/subjects/<topic-name>-value`
