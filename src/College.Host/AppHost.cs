var builder = DistributedApplication.CreateBuilder(args);

// Publishing target: `aspire publish` emits a docker-compose.yml (+ .env) from the
// resources below, so the same model drives `aspire run` locally and `docker compose up`.
builder.AddDockerComposeEnvironment("college");

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("collegedb-data");

var collegedb = postgres.AddDatabase("collegedb");

builder.AddProject<Projects.College_Api>("college-api")
    .WithReference(collegedb)
    .WaitFor(collegedb);

builder.Build().Run();
