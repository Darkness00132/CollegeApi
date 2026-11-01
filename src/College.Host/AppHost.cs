var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("college");

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("collegedb-data");

var collegedb = postgres.AddDatabase("collegedb");

builder.AddProject<Projects.College_Api>("college-api")
    .WithReference(collegedb)
    .WaitFor(collegedb);

builder.Build().Run();
