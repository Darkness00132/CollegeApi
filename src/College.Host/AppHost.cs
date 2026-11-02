var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("college");

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("collegedb-data");

var collegedb = postgres.AddDatabase("collegedb");

var redis = builder.AddRedis("redis")
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.College_Api>("college-api")
    .WithReference(collegedb)
    .WithReference(redis)
    .WaitFor(collegedb)
    .WaitFor(redis);

builder.Build().Run();
